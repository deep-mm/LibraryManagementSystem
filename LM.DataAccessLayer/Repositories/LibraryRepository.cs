/*
 * This repository performs all functions that a user or a librarian performs at the library
 * Author: Deep Mehta
 */
namespace LMS.DataAccessLayer.Repositories
{
    using AutoMapper;
    using LMS.DataAccessLayer.DatabaseContext;
    using LMS.DataAccessLayer.Entities;
    using LMS.SharedFiles.DTOs;
    using Microsoft.Extensions.Caching.Distributed;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class LibraryRepository : ILibraryRepository
    {
        private ReadDBContext readDBContext;
        private IMapper mapper;
        private readonly IDistributedCache distributedCache;
        private string className = "LibraryRepository";

        public LibraryRepository(ReadDBContext readDBContext, IMapper mapper, IDistributedCache distributedCache)
        {
            this.readDBContext = readDBContext;
            this.mapper = mapper;
            this.distributedCache = distributedCache;
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
        public async Task<IEnumerable<BookDTO>> GetAllAvailaibleBooks(string bookName)
        {
            IEnumerable<Book> books = from book in readDBContext.books
                                      join bla in readDBContext.bookLibraryAssociations on book.bookId equals bla.bookId
                                      where bla.isAvailable == true && (book.title.StartsWith(bookName) || string.IsNullOrEmpty(bookName))
                                      select book;

            if (books != null)
            {
                return mapper.Map<IEnumerable<BookDTO>>(books);
            }
            else
            {
                throw new NullReferenceException(className + "/GetAllAvailaibleBooks(): The books array returned from database is null");
            }
        }

        /*
         * Get all the libraries present under the current system: await GetAllLibraries()
         */
        public async Task<IEnumerable<LibraryDTO>> GetAllLibraries()
        {
            IEnumerable<Library> libraries;
            var cachedLibraries = await distributedCache.GetStringAsync($"Library_GetAll");
            if (cachedLibraries == null)
            {
                libraries = from library in readDBContext.libraries
                            select library;

                DistributedCacheEntryOptions options = new DistributedCacheEntryOptions();
                options.SetAbsoluteExpiration(new System.TimeSpan(24, 0, 0));

                await distributedCache.SetStringAsync($"Library_GetAll", JsonConvert.SerializeObject(libraries));
            }
            else
            {
                libraries = JsonConvert.DeserializeObject<IEnumerable<Library>>(cachedLibraries);
            }

            if (libraries != null)
            {
                return mapper.Map<IEnumerable<LibraryDTO>>(libraries);
            }
            else
            {
                throw new NullReferenceException(className + "/GetAllLibraries() The library array returned from database is null");
            }
        }

        /*
         * Get all libraries at a particular location: await GetLibrariesByLocation(locationId)
         */
        public async Task<IEnumerable<LibraryDTO>> GetLibrariesByLocation(int locationId)
        {
            IEnumerable<Library> libraries;
            var cachedLibraries = await distributedCache.GetStringAsync($"Library_GetByLocation_{locationId}");
            if (cachedLibraries == null)
            {
                libraries = from library in readDBContext.libraries
                            where library.locationId == locationId
                            select library;

                DistributedCacheEntryOptions options = new DistributedCacheEntryOptions();
                options.SetAbsoluteExpiration(new System.TimeSpan(24, 0, 0));

                await distributedCache.SetStringAsync($"Library_GetByLocation_{locationId}", JsonConvert.SerializeObject(libraries));
            }
            else
            {
                libraries = JsonConvert.DeserializeObject<IEnumerable<Library>>(cachedLibraries);
            }

            if (libraries != null)
            {
                return mapper.Map<IEnumerable<LibraryDTO>>(libraries);
            }
            else
            {
                throw new NullReferenceException(className + $"/GetLibrariesByLocation(): The library array returned from database for locationId: {locationId} was not found");
            }

        }

        /*
         * Get a single library object based on its ID: await GetLibraryById(libraryId)
         */
        public async Task<LibraryDTO> GetLibraryById(int librarayId)
        {
            if (librarayId != 0)
            {
                Library library;
                var cachedLibrary = await distributedCache.GetStringAsync($"Library_GetById_{librarayId}");
                if (cachedLibrary == null)
                {
                    library = await readDBContext.libraries.FindAsync(librarayId);

                    DistributedCacheEntryOptions options = new DistributedCacheEntryOptions();
                    options.SetAbsoluteExpiration(new System.TimeSpan(24, 0, 0));

                    await distributedCache.SetStringAsync($"Library_GetById_{librarayId}", JsonConvert.SerializeObject(library));
                }
                else
                {
                    library = JsonConvert.DeserializeObject<Library>(cachedLibrary);
                }

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
            IEnumerable<Location> locations;
            var cachedLocations = await distributedCache.GetStringAsync($"Location_GetAll");
            if (cachedLocations == null)
            {
                locations = from location in readDBContext.locations
                            select location;

                DistributedCacheEntryOptions options = new DistributedCacheEntryOptions();
                options.SetAbsoluteExpiration(new System.TimeSpan(24, 0, 0));

                await distributedCache.SetStringAsync($"Location_GetAll", JsonConvert.SerializeObject(locations));
            }
            else
            {
                locations = JsonConvert.DeserializeObject<IEnumerable<Location>>(cachedLocations);
            }

            if (locations != null)
            {
                IEnumerable<LocationDTO> locationDTOs = mapper.Map<IEnumerable<LocationDTO>>(locations);
                foreach (var location in locationDTOs)
                {
                    IEnumerable<LibraryDTO> libraries = await GetLibrariesByLocation(location.locationId);
                    if (libraries != null && libraries.Count() > 0)
                    {
                        location.libraryId = libraries.ToList()[0].libraryId;
                    }
                    else if (libraries == null)
                    {
                        throw new NullReferenceException(className + "/GetAllLocations(): libraries array returned as null from the dataAcessLayer");
                    }
                }
                return locationDTOs;
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
