using LMS.SharedFiles.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.MVC.Services
{
    public interface ILibraryRepository
    {
        Task<IEnumerable<BookDTO>> GetAvailaibleBooks();

        Task<bool> CheckoutBook(int bookId, int userId);

        Task<bool> ReturnBook(int bookId, int userId);
    }
}
