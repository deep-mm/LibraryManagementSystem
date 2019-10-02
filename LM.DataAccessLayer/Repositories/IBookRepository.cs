/*
 * Books Repositories
 * Author: Deep Mehta
 */
namespace LMS.DataAccessLayer.Repositories
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using LMS.SharedFiles.DTOs;

    public interface IBookRepository
    {
        // To obtain list of books starting with a particular name or all books if bookName is empty
        Task<IEnumerable<BookDTO>> GetBookByName(string bookName, int libraryId);

        // To Add a new book object to the database
        Task<bool> AddNewBook(BookDTO newBook, int libraryId);

        // To obtain a book object by its bookId
        Task<BookDTO> GetBookById(int id);

        // To delete a particular book row from the database
        Task<bool> DeleteBook(int id);

        // To update book details in the database
        Task<bool> UpdateBook(BookDTO newBook);
    }
}
