/*
 * This repository contains all operations to be performed on the database related to libraries
 * Author: Deep Mehta
 */
namespace LMS.DataAccessLayer.Repositories
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using LMS.DataAccessLayer.Entities;
    using System.Collections;
    using LMS.SharedFiles.DTOs;

    public interface ILibraryRepository
    {
        // Get all the libraries in the database
        Task<IEnumerable<LibraryDTO>> GetAllLibraries();

        // Get all the libraries in the database
        Task<IEnumerable<LocationDTO>> GetAllLocations();

        // Get a library object with id as libraryId
        Task<LibraryDTO> GetLibraryById(int librarayId);

        // Get all libraries at a particular location
        Task<IEnumerable<LibraryDTO>> GetLibrariesByLocation(int locationId);

        // Get all the availaible Books
        Task<IEnumerable<BookDTO>> GetAllAvailaibleBooks();

        // Checkout a book from the library
        Task<bool> CheckoutBook(int bookId, int userId);

        // Return a book to the library
        Task<bool> ReturnBook(int bookId, int userId);
    }
}
