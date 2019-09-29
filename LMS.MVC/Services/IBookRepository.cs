using LMS.SharedFiles.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.MVC.Services
{
    public interface IBookRepository
    {
        Task<IEnumerable<BookDTO>> GetBooks(String searchTerm);

        Task<BookDTO> GetBookById(int id);

        Task<bool> AddBook(BookDTO newBook, int libraryId);

        Task<bool> DeleteBook(int bookId);

        Task<bool> EditBook(BookDTO updatedBook);

        Task<string> UploadImage(BookImageDTO bookImageDTO);
    }
}
