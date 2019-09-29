/*
 * This repository performs all functions that a user or a librarian performs at the library
 * Author: Deep Mehta
 */
namespace LMS.DataAccessLayer.Repositories
{
    using LMS.DataAccessLayer.Entities;
    using System.Collections.Generic;
    using System.Linq;
    using LMS.DataAccessLayer.Repositories;
    using LMS.SharedFiles.DTOs;
    using System.Threading.Tasks;
    using System;
    using System.Collections;
    using AutoMapper;
    using LMS.DataAccessLayer.DatabaseContext;
    using Microsoft.EntityFrameworkCore;
    using LMS.DataAccessLayer.Profiles;

    public class LibraryServices : ILibraryRepository
    {
        private ReadDBContext readDBContext;
        private IMapper mapper;

        public LibraryServices()
        {
            Initialize();
        }

        public void Initialize()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ReadDBContext>();
            string conn = "Server=tcp:tone-app.database.windows.net,1433;Initial Catalog=LMS;Persist Security Info=False;User ID=demeht;Password=India@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            optionsBuilder.UseSqlServer(conn);

            ReadDBContext context = new ReadDBContext(optionsBuilder.Options);
            this.readDBContext = context;


            var config = new MapperConfiguration(cfg =>
                cfg.AddProfile(new AutoMapperProfiles())
            );

            this.mapper = config.CreateMapper();
        }
        /*
         * User checks out a book from the library: await CheckoutBook(bookId,userId)
         */
        public async Task<bool> CheckoutBook(int bookId, int userId)
        {
            //Getting BookLibraryAssociation data object with the same bookId
            var bookLibraryAssociation = (from bla in readDBContext.bookLibraryAssociations
                                          join book in readDBContext.books on bla.bookId equals book.bookId
                                          select bla).FirstOrDefault();
            //Change book status since book has been checked out
            bookLibraryAssociation.isAvailable = false;
            bookLibraryAssociation.isCheckedOut = true;

            //Make a new entry in UserBookAssociation Table in the database
            UserBookAssociation userBookAssociation = new UserBookAssociation();
            userBookAssociation.BookLibraryAssociationId = bookLibraryAssociation.bookLibraryAssociationId;
            userBookAssociation.userId = userId;
            userBookAssociation.DueDate = DateTime.Now.AddDays(90);
            await readDBContext.userBookAssociations.AddAsync(userBookAssociation);
            await Commit();
            return true;
        }

        /*
         * Get all books with availaibility status as true: await GetAllAvailaibleBooks()
         */
        public async Task<IEnumerable<BookDTO>> GetAllAvailaibleBooks()
        {
            IEnumerable<Book> books = from book in readDBContext.books
                                      join bla in readDBContext.bookLibraryAssociations on book.bookId equals bla.bookId
                                      where bla.isAvailable == true
                                      select book;

            return mapper.Map<IEnumerable<BookDTO>>(books);
        }

        /*
         * Get all the libraries present under the current system: await GetAllLibraries()
         */
        public async Task<IEnumerable<LibraryDTO>> GetAllLibraries()
        {
            IEnumerable<Library> libraries = from library in readDBContext.libraries
                                             select library;

            return mapper.Map<IEnumerable<LibraryDTO>>(libraries);
        }

        /*
         * Get all libraries at a particular location: await GetLibrariesByLocation(locationId)
         */
        public async Task<IEnumerable<LibraryDTO>> GetLibrariesByLocation(int locationId)
        {
            IEnumerable<Library> libraries = from library in readDBContext.libraries
                                             where library.locationId == locationId
                                             select library;

            return mapper.Map<IEnumerable<LibraryDTO>>(libraries);
        }

        /*
         * Get a single library object based on its ID: await GetLibraryById(libraryId)
         */
        public async Task<LibraryDTO> GetLibraryById(int librarayId)
        {
            Library library = await readDBContext.libraries.FindAsync(librarayId);
            return mapper.Map<LibraryDTO>(library);
        }

        /*
         * User returns a checked out book back to the library: await ReturnBook(bookId
         */
        public async Task<bool> ReturnBook(int bookId, int userId)
        {
            //Getting BookLibraryAssociation data object with the same bookId
            var bookLibraryAssociation = (from bla in readDBContext.bookLibraryAssociations
                                          join book in readDBContext.books on bla.bookId equals book.bookId
                                          join uba in readDBContext.userBookAssociations on bla.bookLibraryAssociationId equals uba.BookLibraryAssociationId
                                          where uba.userId == userId
                                          select bla).SingleOrDefault();
            //Change book status since book has been checked out
            bookLibraryAssociation.isAvailable = true;
            bookLibraryAssociation.isCheckedOut = false;

            UserBookAssociation userBookAssociation = readDBContext.userBookAssociations.Where
                (u => u.BookLibraryAssociationId == bookLibraryAssociation.bookLibraryAssociationId).SingleOrDefault();
            readDBContext.userBookAssociations.Remove(userBookAssociation);
            await Commit();
            return true;
        }

        /*
         * Get all locations where different libraries operate at
         */
        public async Task<IEnumerable<LocationDTO>> GetAllLocations()
        {
            IEnumerable<Location> locations = from location in readDBContext.locations
                                              select location;
            return mapper.Map<IEnumerable<LocationDTO>>(locations);
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
