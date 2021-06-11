
namespace LMS.BusinessLogic.Services
{
    using LMS.DataAccessLayer.Repositories;
    using LMS.SharedFiles.DTOs;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class BooksBusinessLogic : IBooksBusinessLogic
    {
        private readonly IBookRepository bookRepository;
        private readonly IBlobRepository blobRepository;

        public BooksBusinessLogic(IBookRepository bookRepository, IBlobRepository blobRepository)
        {
            this.bookRepository = bookRepository;
            this.blobRepository = blobRepository;
        }

        public async Task<IEnumerable<BookDTO>> GetBookByName(string bookName, int libraryId)
        {
            IEnumerable<BookDTO> bookDTOs = await bookRepository.GetBookByName(bookName, libraryId);
            return bookDTOs;
        }

        // To Add a new book object to the database
        public async Task<bool> AddNewBook(BookDTO newBook, int libraryId)
        {
            return await bookRepository.AddNewBook(newBook, libraryId);
        }

        // To obtain a book object by its bookId
        public async Task<BookDTO> GetBookById(int id)
        {
            BookDTO bookDTO = await bookRepository.GetBookById(id);
            return bookDTO;
        }

        // To delete a particular book row from the database
        public async Task<bool> DeleteBook(int id)
        {
            return await bookRepository.DeleteBook(id);
        }

        // To update book details in the database
        public async Task<bool> UpdateBook(BookDTO newBook)
        {
            return await bookRepository.UpdateBook(newBook);
        }

        public async Task<string> UploadImage(BookImageDTO bookImageDTO, string BlobName)
        {
            return await blobRepository.UploadImage(bookImageDTO, BlobName);
        }
    }
}
