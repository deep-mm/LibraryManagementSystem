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
    using LMS.DataAccessLayer.Profiles;
    using LMS.SharedFiles.DTOs;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Azure.Storage;
    using Microsoft.Azure.Storage.Blob;
    using Microsoft.EntityFrameworkCore;

    public class BookServices : IBookRepository
    {
        private ReadDBContext readDBContext;
        private IMapper mapper;
        public CloudBlobContainer cloudBlobContainer;

        public async Task Initialize()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ReadDBContext>();
            string conn = "Server=tcp:tone-app.database.windows.net,1433;Initial Catalog=LMS;Persist Security Info=False;User ID=demeht;Password=India@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            optionsBuilder.UseSqlServer(conn);
            optionsBuilder.EnableSensitiveDataLogging();

            ReadDBContext context = new ReadDBContext(optionsBuilder.Options);
            this.readDBContext = context;


            var config = new MapperConfiguration(cfg =>
                cfg.AddProfile(new AutoMapperProfiles())
            );

            this.mapper = config.CreateMapper();

            string azureStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=lmstone;AccountKey=54TPoXv2wFgzWnBQfT5/E2L/CLfbAHdk8sBzh86slPw14XjoJAKBFT7dfQQKNxZFxUIETYsD6F+vFcKMnqFKiQ==;EndpointSuffix=core.windows.net;";
            CloudStorageAccount storageAccount;
            if (CloudStorageAccount.TryParse(azureStorageConnectionString, out storageAccount))
            {
                CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

                cloudBlobContainer = cloudBlobClient.GetContainerReference("bookimagecontainer");
                await cloudBlobContainer.CreateIfNotExistsAsync();

                BlobContainerPermissions permissions = new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                };

                await cloudBlobContainer.SetPermissionsAsync(permissions);

            }
            else
            {
                Console.WriteLine("Connection String is invalid");
            }
        }

        public BookServices()
        {
            Initialize();
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
            BookDTO toDelete = await GetBookById(id);//Get the book details by searching its ID as primary key in the books table
            if (toDelete != null)
            {
                Book mappedBook = mapper.Map<Book>(toDelete); //Mapping BookDTO to Book
                BookLibraryAssociation bookLibraryAssociation = readDBContext.bookLibraryAssociations.Where(b => b.bookId == mappedBook.bookId).FirstOrDefault();
                bool availaibilityStatus = bookLibraryAssociation.isAvailable;
                if (availaibilityStatus)
                {
                    readDBContext.bookLibraryAssociations.Remove(bookLibraryAssociation);
                    await Commit();
                    readDBContext.books.Remove(mappedBook); //Deleting a book from DB
                    await Commit();
                }
                else
                    return false;
            }
            return true;
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

        public async Task<string> UploadImage(BookImageDTO bookImageDTO, string BlobName)
        {
            //cloudBlockBlob.UploadFromByteArrayAsync(bookImageDTO.bookImage,0,bookImageDTO.bookImage.Length-1).Wait();
            BookDTO book = bookImageDTO.bookDTO;
            CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(book.bookId.ToString() + ".png");
            cloudBlockBlob.Metadata.Add("bookId", book.bookId.ToString());
            cloudBlockBlob.Metadata.Add("bookName", book.title);
            cloudBlockBlob.UploadFromByteArrayAsync(bookImageDTO.bookImage,0,bookImageDTO.bookImage.Length-1).Wait();
            string uri = cloudBlockBlob.Uri.ToString();
            return uri;
        }
    }
       
}
