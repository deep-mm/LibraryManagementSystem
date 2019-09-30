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

    public class LibraryRepository : ILibraryRepository
    {
        private ReadDBContext readDBContext;
        private IMapper mapper;
        private string className = "LibraryRepository";

        public LibraryRepository(ReadDBContext readDBContext, IMapper mapper)
        {
            this.readDBContext = readDBContext;
            this.mapper = mapper;
        }

        /*
         * User checks out a book from the library: await CheckoutBook(bookId,userId)
         */
        public async Task<bool> CheckoutBook(int bookId, int userId)
        {
            if (bookId == 0)
            {
                throw new ArgumentNullException(className + "/CheckoutBook(): The bookId parameter received is null");
            }
            else if (userId == 0)
            {
                throw new ArgumentNullException(className + "/CheckoutBook(): The userId parameter received is null");
            }
            else
            {
                //Getting BookLibraryAssociation data object with the same bookId
                var bookLibraryAssociation = (from bla in readDBContext.bookLibraryAssociations
                                              where bla.bookId == bookId
                                              select bla).FirstOrDefault();

                if (bookLibraryAssociation != null)
                {
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
                else
                {
                    throw new NullReferenceException(className + $"/CheckoutBook():The bookLibraryAssociation for bookId: {bookId} was not found");
                }
            }
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

            if (books != null)
            {
                return mapper.Map<IEnumerable<BookDTO>>(books);
            }
            else
            {
                throw new NullReferenceException(className+ "/GetAllAvailaibleBooks(): The books array returned from database is null");
            }
        }

        /*
         * Get all the libraries present under the current system: await GetAllLibraries()
         */
        public async Task<IEnumerable<LibraryDTO>> GetAllLibraries()
        {
            IEnumerable<Library> libraries = from library in readDBContext.libraries
                                             select library;

            if (libraries != null)
            {
                return mapper.Map<IEnumerable<LibraryDTO>>(libraries);
            }
            else
            {
                throw new NullReferenceException(className+ "/GetAllLibraries() The library array returned from database is null");
            }
        }

        /*
         * Get all libraries at a particular location: await GetLibrariesByLocation(locationId)
         */
        public async Task<IEnumerable<LibraryDTO>> GetLibrariesByLocation(int locationId)
        {
            IEnumerable<Library> libraries = from library in readDBContext.libraries
                                             where library.locationId == locationId
                                             select library;

            if (libraries != null)
            {
                return mapper.Map<IEnumerable<LibraryDTO>>(libraries);
            }
            else
            {
                throw new NullReferenceException(className+ $"/GetLibrariesByLocation(): The library array returned from database for locationId: {locationId} was not found");
            }

        }

        /*
         * Get a single library object based on its ID: await GetLibraryById(libraryId)
         */
        public async Task<LibraryDTO> GetLibraryById(int librarayId)
        {
            if (librarayId != 0)
            {
                Library library = await readDBContext.libraries.FindAsync(librarayId);
                if (library != null)
                {
                    return mapper.Map<LibraryDTO>(library);
                }
                else
                {
                    throw new NullReferenceException(className + $"/GetLibraryById(): The library object for libraryId:{librarayId} was not found");
                }
            }
            else
            {
                throw new ArgumentNullException(className + "/GetLibraryById(): The libraryId parameter received is null");
            }
        }

        /*
         * User returns a checked out book back to the library: await ReturnBook(bookId,userId)
         */
        public async Task<bool> ReturnBook(int bookId, int userId)
        {
            if (bookId == 0)
            {
                throw new ArgumentNullException(className + "/ReturnBook(): The bookId parameter received is null");
            }
            else if (userId == 0)
            {
                throw new ArgumentNullException(className + "/ReturnBook(): The libraryId parameter received is null");
            }
            else
            {
                //Getting BookLibraryAssociation data object with the same bookId
                var bookLibraryAssociation = (from bla in readDBContext.bookLibraryAssociations
                                              join uba in readDBContext.userBookAssociations on bla.bookLibraryAssociationId equals uba.BookLibraryAssociationId
                                              where uba.userId == userId && bla.bookId == bookId
                                              select bla).SingleOrDefault();

                if (bookLibraryAssociation != null)
                {
                    //Change book status since book has been checked out
                    bookLibraryAssociation.isAvailable = true;
                    bookLibraryAssociation.isCheckedOut = false;

                    UserBookAssociation userBookAssociation = readDBContext.userBookAssociations.Where
                        (u => u.BookLibraryAssociationId == bookLibraryAssociation.bookLibraryAssociationId).SingleOrDefault();

                    if (userBookAssociation != null)
                    {
                        readDBContext.userBookAssociations.Remove(userBookAssociation);
                        await Commit();
                        return true;
                    }
                    else
                    {
                        throw new NullReferenceException(className + $"/ReturnBook(): The userBookAssociation for bookLibraryAssociationId:{bookLibraryAssociation.bookLibraryAssociationId} was not found");
                    }
                }
                else
                {
                    throw new NullReferenceException(className + $"/ReturnBook(): The bookLibraryAssociation for bookId:{bookId} was not found");
                }
            }
        }

        /*
         * Get all locations where different libraries operate at
         */
        public async Task<IEnumerable<LocationDTO>> GetAllLocations()
        {
            IEnumerable<Location> locations = from location in readDBContext.locations
                                              select location;

            if (locations != null)
            {
                return mapper.Map<IEnumerable<LocationDTO>>(locations);
            }
            else
            {
                throw new NullReferenceException(className + "/GetAllLocations(): The locations array received from database is null");
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
                throw new Exception(className + "/Commit(): Error occured while commiting to database");
            }
        }

    }
}
