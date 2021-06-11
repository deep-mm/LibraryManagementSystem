using LMS.DataAccessLayer.Repositories;
using LMS.SharedFiles.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS.BusinessLogic.Services
{
    public class LibraryBusinessLogic : ILibraryBusinessLogic
    {
        private readonly ILibraryRepository libraryRepository;
        private readonly IDiscussionRepository discussionRepository;
        private readonly IAzureSearchService azureSearchService;

        public LibraryBusinessLogic(ILibraryRepository libraryRepository, IDiscussionRepository discussionRepository, IAzureSearchService azureSearchService)
        {
            this.libraryRepository = libraryRepository;
            this.discussionRepository = discussionRepository;
            this.azureSearchService = azureSearchService;
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
        public async Task<IEnumerable<BookDTO>> GetAllAvailaibleBooks(string bookName)
        {
            return await libraryRepository.GetAllAvailaibleBooks(bookName);
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

        public async Task<bool> AddNewPost(PostDTO post)
        {
            return await discussionRepository.AddNewPost(post);
        }

        public async Task<IEnumerable<PostDTO>> GetAllPosts()
        {
            return await discussionRepository.GetAllPosts();
        }

        public async Task<IEnumerable<PostDTO>> SearchPost(string searchTerm)
        {
            return await azureSearchService.Search(searchTerm);
        }
    }
}
