using LMS.DataAccessLayer.Repositories;
using LMS.SharedFiles.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.BusinessLogic.Services
{
    public class LibraryBusinessLogic
    {
        private readonly ILibraryRepository libraryRepository;

        public LibraryBusinessLogic(ILibraryRepository libraryRepository)
        {
            this.libraryRepository = libraryRepository;
        }

        // Get all the libraries in the database
        public async Task<IEnumerable<LibraryDTO>> GetAllLibraries()
        {
            try
            {
                return await libraryRepository.GetAllLibraries();
            }
            catch (Exception e)
            {
                //TODO: Log Exception in ILogger,File,Application Insights
                return null;
            }
        }

        // Get all the libraries in the database
        public async Task<IEnumerable<LocationDTO>> GetAllLocations()
        {
            try
            {
                return await libraryRepository.GetAllLocations();
            }
            catch (Exception e)
            {
                //TODO: Log Exception in ILogger,File,Application Insights
                return null;
            }
        }

        // Get a library object with id as libraryId
        public async Task<LibraryDTO> GetLibraryById(int librarayId)
        {
            try
            {
                if (librarayId != 0)
                {
                    return await libraryRepository.GetLibraryById(librarayId);
                }
                else
                {
                    //throw new exception
                }
                return null;
            }
            catch (Exception e)
            {
                //TODO: Log Exception in ILogger,File,Application Insights
                return null;
            }
        }

        // Get all libraries at a particular location
        public async Task<IEnumerable<LibraryDTO>> GetLibrariesByLocation(int locationId)
        {
            try
            {
                if (locationId != 0)
                {
                    return await libraryRepository.GetLibrariesByLocation(locationId);
                }
                else
                {
                    //throw new exception
                }
                return null;
            }
            catch (Exception e)
            {
                //TODO: Log Exception in ILogger,File,Application Insights
                return null;
            }
        }

        // Get all the availaible Books
        public async Task<IEnumerable<BookDTO>> GetAllAvailaibleBooks()
        {
            try
            {
                return await libraryRepository.GetAllAvailaibleBooks();
            }
            catch (Exception e)
            {
                //TODO: Log Exception in ILogger,File,Application Insights
                return null;
            }
        }

        // Checkout a book from the library
        public async Task<bool> CheckoutBook(int bookId, int userId)
        {
            try
            {
                if (bookId != 0 && userId != 0)
                {
                    return await libraryRepository.CheckoutBook(bookId,userId);
                }
                else if(bookId==0)
                {
                    //throw new exception
                }
                else if (userId == 0)
                {
                    //throw new exception
                }

                return false;
            }
            catch (Exception e)
            {
                //TODO: Log Exception in ILogger,File,Application Insights
                return false;
            }
        }

        // Return a book to the library
        public async Task<bool> ReturnBook(int bookId, int userId)
        {
            try
            {
                if (bookId != 0 && userId != 0)
                {
                    return await libraryRepository.ReturnBook(bookId, userId);
                }
                else if (bookId == 0)
                {
                    //throw new exception
                }
                else if (userId == 0)
                {
                    //throw new exception
                }

                return false;
            }
            catch (Exception e)
            {
                //TODO: Log Exception in ILogger,File,Application Insights
                return false;
            }
        }
    }
}
