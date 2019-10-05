/*
    * This Repository contains methods to interact with the books data in the sql database - CRUD Operations on Books
    * Author: Deep Mehta
 */
namespace LMS.DataAccessLayer.Repositories
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using LMS.DataAccessLayer.DatabaseContext;
    using LMS.DataAccessLayer.Entities;
    using LMS.SharedFiles.DTOs;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;
    using Newtonsoft.Json;

    public class BookRepository : IBookRepository
    {
        private ReadDBContext readDBContext;
        private IMapper mapper;
        private readonly IDistributedCache distributedCache;
        private string className = "BookRepository";

        public BookRepository(ReadDBContext readDBContext, IMapper mapper, IDistributedCache distributedCache)
        {
            this.readDBContext = readDBContext;
            this.mapper = mapper;
            this.distributedCache = distributedCache;
        }

        /*
         * Adding a new Book to the database: await addNewBook(book)
         */
        public async Task<bool> AddNewBook(BookDTO newBook, int libraryId)
        {
            if (newBook == null)
            {
                throw new ArgumentNullException(className + "/AddNewBook(): The newBook object parameter is null");
            }
            else if (libraryId == 0)
            {
                throw new ArgumentNullException(className + "/AddNewBook(): The libraryId parameter is null");
            }
            else
            {
                Book mappedBook = mapper.Map<Book>(newBook); //Mapping BookDTO to Book
                await readDBContext.books.AddAsync(mappedBook); //Adding book to DB
                await Commit();

                //Creating a new BookLibraryAssociation to insert into the DB
                BookLibraryAssociation bookLibraryAssociation = new BookLibraryAssociation();
                bookLibraryAssociation.bookId = mappedBook.bookId;
                bookLibraryAssociation.libraryId = libraryId;
                bookLibraryAssociation.isAvailable = true;
                bookLibraryAssociation.isCheckedOut = false;

                await readDBContext.bookLibraryAssociations.AddAsync(bookLibraryAssociation); //Adding book to DB
                await Commit();
                await ClearCache($"Books_AllBooks_{""}");
                return true;
            }
        }

        /*
         * Delete a book from the database: await deleteBook(bookId)
         */
        public async Task<bool> DeleteBook(int id)
        {
            if (id != 0)
            {
                Book mappedBook = await readDBContext.books.FindAsync(id);
                if (mappedBook != null)
                {
                    BookLibraryAssociation bookLibraryAssociation = readDBContext.bookLibraryAssociations.Where(b => b.bookId == mappedBook.bookId).FirstOrDefault();
                    if (bookLibraryAssociation != null)
                    {
                        bool availaibilityStatus = bookLibraryAssociation.isAvailable;
                        if (availaibilityStatus)
                        {
                            readDBContext.bookLibraryAssociations.Remove(bookLibraryAssociation);
                            await Commit();
                            readDBContext.books.Remove(mappedBook); //Deleting a book from DB
                            await Commit();
                            await ClearCache($"Books_AllBooks_{""}");
                            return true;
                        }
                        else
                            return false;
                    }
                    else
                    {
                        readDBContext.Remove(mappedBook); //Deleting a book from DB
                        await Commit();
                        await ClearCache($"Books_AllBooks_{""}");
                        return true;
                    }
                }
                else
                {
                    throw new NullReferenceException(className + $"/DeleteBook(): The Book for id: {id} was not found");
                }
            }
            else
            {
                throw new ArgumentNullException(className + "/DeleteBook(): The id parameter received is null");
            }
        }

        /*
         * Finding an book object by its ID: await getBookById(bookId)
         */
        public async Task<BookDTO> GetBookById(int id)
        {
            if (id != 0)
            {
                Book book;
                var cachedBook = await distributedCache.GetStringAsync($"Books_BookId_{id}");

                if (cachedBook == null)
                {
                    book = await readDBContext.books.FindAsync(id);

                    DistributedCacheEntryOptions options = new DistributedCacheEntryOptions();
                    options.SetAbsoluteExpiration(new System.TimeSpan(24, 0, 0));

                    await distributedCache.SetStringAsync($"Books_BookId_{id}", JsonConvert.SerializeObject(book));
                }
                else
                {
                    book = JsonConvert.DeserializeObject<Book>(cachedBook);
                }
                if (book != null)
                {
                    BookDTO mappedDTO = mapper.Map<BookDTO>(book);
                    return mappedDTO;
                }
                else
                {
                    throw new NullReferenceException(className + $"/GetBookById(): The Book for id: {id} was not found");
                }
            }
            else
            {
                throw new ArgumentNullException(className + "/GetBookById(): The id parameter received is null");
            }
        }

        /*
         * If bookName is empty, return all books
         * If bookName has a value, return list of those books starting ith that value
         * await getBookByName(Harry)
         */
        public async Task<IEnumerable<BookDTO>> GetBookByName(string bookName, int libraryId)
        {
            if (libraryId == 0)
            {
                throw new ArgumentNullException(className + "/GetBookByName(): The libraryId parameter received as null");
            }
            if (bookName != null)
            {
                IEnumerable<Book> books;
                string cachedBooks = null;
                if (bookName.Equals(""))
                    cachedBooks = await distributedCache.GetStringAsync($"Books_AllBooks_{bookName}_{libraryId}");

                if (cachedBooks == null)
                {
                    var query = from b in readDBContext.books
                                join bla in readDBContext.bookLibraryAssociations on b.bookId equals bla.bookId
                                where (b.title.StartsWith(bookName) || string.IsNullOrEmpty(bookName)) && bla.libraryId == libraryId
                                orderby b.title
                                select b;

                    books = query.AsEnumerable<Book>();

                    DistributedCacheEntryOptions options = new DistributedCacheEntryOptions();
                    options.SetAbsoluteExpiration(new System.TimeSpan(24, 0, 0));

                    if(bookName.Equals(""))
                        await distributedCache.SetStringAsync($"Books_AllBooks_{bookName}_{libraryId}", JsonConvert.SerializeObject(books));
                }
                else
                {
                    books = JsonConvert.DeserializeObject<IEnumerable<Book>>(cachedBooks);
                }
                if (books != null)
                {
                    IEnumerable<BookDTO> bookDTOs = mapper.Map<IEnumerable<BookDTO>>(books);
                    return bookDTOs;
                }
                else
                {
                    throw new NullReferenceException(className + $"/GetBookByName(): The Book for name: {bookName} was not found");
                }
            }
            else
            {
                throw new ArgumentNullException(className + "/GetBookByName(): The bookName parameter received is null");
            }
        }

        /*
         * Update a book object in the database: await updateBook(newBookObject)
         */
        public async Task<bool> UpdateBook(BookDTO newBook)
        {
            if (newBook != null)
            {
                Book mappedBook = mapper.Map<Book>(newBook);
                var entity = readDBContext.books.Attach(mappedBook);
                //readDBContext.Update(newBook);
                entity.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await Commit();
                await ClearCache($"Books_AllBooks_{""}");
                await ClearCache($"Books_BookId_{newBook.bookId}");
                return true;
            }
            else
            {
                throw new ArgumentNullException(className + "/UpdateBook(): The newBook object parameter received is null");
            }
        }

        /*
         * Commiting all changes made locally to the database: await commit()
         */
        public async Task Commit()
        {
            try
            {
                await readDBContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(className + "/Commit(): Error occured while commiting to database + Exception: "+e.ToString());
            }
        }

        public async Task ClearCache(string key)
        {
            await distributedCache.RemoveAsync(key);
        }
    }
       
}
