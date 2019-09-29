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
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;

    public class BookRepository : IBookRepository
    {
        private ReadDBContext readDBContext;
        private IMapper mapper;

        public BookRepository(ReadDBContext readDBContext, IMapper mapper)
        {
            this.readDBContext = readDBContext;
            this.mapper = mapper;
        }

        /*
         * Adding a new Book to the database: await addNewBook(book)
         */
        public async Task<bool> AddNewBook(BookDTO newBook, int libraryId)
        {
            Book mappedBook = mapper.Map<Book>(newBook); //Mapping BookDTO to Book
            await readDBContext.books.AddAsync(mappedBook); //Adding book to DB
            await Commit();
            BookLibraryAssociation bookLibraryAssociation = new BookLibraryAssociation();
            bookLibraryAssociation.bookId = mappedBook.bookId;
            bookLibraryAssociation.libraryId = libraryId;
            bookLibraryAssociation.isAvailable = true;
            bookLibraryAssociation.isCheckedOut = false;
            await readDBContext.bookLibraryAssociations.AddAsync(bookLibraryAssociation); //Adding book to DB
            await Commit();
            return true;
        }

        /*
         * Delete a book from the database: await deleteBook(bookId)
         */
        public async Task<bool> DeleteBook(int id)
        {
                Book mappedBook = await readDBContext.books.FindAsync(id);
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
                        return true;
                    }
                    else
                        return false;
                }
                else
                {
                    readDBContext.Remove(mappedBook); //Deleting a book from DB
                    await Commit();
                    return true;
                    //return false;
                }
        }

        /*
         * Finding an book object by its ID: await getBookById(bookId)
         */
        public async Task<BookDTO> GetBookById(int id)
        {
            Book book = await readDBContext.books.FindAsync(id);
            BookDTO mappedDTO = mapper.Map<BookDTO>(book);
            if (book != null)
            {
                return mappedDTO;
            }
            return null;
        }

        /*
         * If bookName is empty, return all books
         * If bookName has a value, return list of those books starting ith that value
         * await getBookByName(Harry)
         */
        public async Task<IEnumerable<BookDTO>> GetBookByName(string bookName)
        {
            var query = from b in readDBContext.books
                        where b.title.StartsWith(bookName) || string.IsNullOrEmpty(bookName)
                        orderby b.title
                        select b;
            IEnumerable<Book> books = query.AsEnumerable<Book>();

            IEnumerable<BookDTO> bookDTOs = mapper.Map<IEnumerable<BookDTO>>(books);
            return bookDTOs;
        }

        /*
         * Update a book object in the database: await updateBook(newBookObject)
         */
        public async Task<bool> UpdateBook(BookDTO newBook)
        {
            Book mappedBook = mapper.Map<Book>(newBook);
            var entity = readDBContext.books.Attach(mappedBook);
            //readDBContext.Update(newBook);
            entity.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await Commit();
            return true;
        }

        /*
         * Commiting all changes made locally to the database: await commit()
         */
        public async Task Commit()
        {
            await readDBContext.SaveChangesAsync();
        }
    }
       
}
