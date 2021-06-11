using LMS.SharedFiles.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS.BusinessLogic.Services
{
    public interface IBooksBusinessLogic
    {
        Task<bool> AddNewBook(BookDTO newBook, int libraryId);
        Task<bool> DeleteBook(int id);
        Task<BookDTO> GetBookById(int id);
        Task<IEnumerable<BookDTO>> GetBookByName(string bookName, int libraryId);
        Task<bool> UpdateBook(BookDTO newBook);
        Task<string> UploadImage(BookImageDTO bookImageDTO, string BlobName);
    }
}