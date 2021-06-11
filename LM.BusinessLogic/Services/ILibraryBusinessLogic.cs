using LMS.SharedFiles.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS.BusinessLogic.Services
{
    public interface ILibraryBusinessLogic
    {
        Task<bool> CheckoutBook(int bookId, int userId);
        Task<IEnumerable<BookDTO>> GetAllAvailaibleBooks(string bookName);
        Task<IEnumerable<LibraryDTO>> GetAllLibraries();
        Task<IEnumerable<LocationDTO>> GetAllLocations();
        Task<IEnumerable<LibraryDTO>> GetLibrariesByLocation(int locationId);
        Task<LibraryDTO> GetLibraryById(int librarayId);
        Task<bool> ReturnBook(int bookId, int userId);
        Task<IEnumerable<PostDTO>> GetAllPosts();
        Task<bool> AddNewPost(PostDTO post);
        Task<IEnumerable<PostDTO>> SearchPost(string searchTerm);
    }
}