﻿/*
 * This repository contains all operations to be performed on the database related to libraries
 * Author: Deep Mehta
 */
namespace LMS.DataAccessLayer.Repositories
{
    using LMS.SharedFiles.DTOs;
    using System.Collections.Generic;
    using System.Threading.Tasks;

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
        Task<IEnumerable<BookDTO>> GetAllAvailaibleBooks(string bookName);

        // Checkout a book from the library
        Task<bool> CheckoutBook(int bookId, int userId);

        // Return a book to the library
        Task<bool> ReturnBook(int bookId, int userId);
    }
}
