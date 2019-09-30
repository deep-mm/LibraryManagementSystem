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
            return await libraryRepository.GetAllLibraries();
        }

        // Get all the libraries in the database
        public async Task<IEnumerable<LocationDTO>> GetAllLocations()
        {
            return await libraryRepository.GetAllLocations();
        }

        // Get a library object with id as libraryId
        public async Task<LibraryDTO> GetLibraryById(int librarayId)
        {
            return await libraryRepository.GetLibraryById(librarayId);
        }

        // Get all libraries at a particular location
        public async Task<IEnumerable<LibraryDTO>> GetLibrariesByLocation(int locationId)
        {
            return await libraryRepository.GetLibrariesByLocation(locationId);
        }

        // Get all the availaible Books
        public async Task<IEnumerable<BookDTO>> GetAllAvailaibleBooks()
        {
            return await libraryRepository.GetAllAvailaibleBooks();
        }

        // Checkout a book from the library
        public async Task<bool> CheckoutBook(int bookId, int userId)
        {
            return await libraryRepository.CheckoutBook(bookId, userId);
        }

        // Return a book to the library
        public async Task<bool> ReturnBook(int bookId, int userId)
        {
            return await libraryRepository.ReturnBook(bookId, userId);
        }
    }
}
