
namespace LMS.BusinessLogic.Services
{
    using LMS.DataAccessLayer.Repositories;
    using LMS.SharedFiles.DTOs;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class BooksBusinessLogic
    {
        private readonly IBookRepository bookRepository;
        private readonly IBlobRepository blobRepository;

        public BooksBusinessLogic(IBookRepository bookRepository, IBlobRepository blobRepository)
        {
            this.bookRepository = bookRepository;
            this.blobRepository = blobRepository;
        }

        public async Task<IEnumerable<BookDTO>> GetBookByName(string bookName)
        {
            try
            {
                if (bookName != null)
                {
                    IEnumerable<BookDTO> bookDTOs = await bookRepository.GetBookByName(bookName);
                    return bookDTOs;
                }

                else
                {
                    //TODO: Throw an exception
                }
                return null;
            }
            catch(Exception e)
            {
                //TODO: Log Exception in ILogger,File,Application Insights
                Console.WriteLine(e.StackTrace);
                return null;
            }
                
        }

        // To Add a new book object to the database
        public async Task<bool> AddNewBook(BookDTO newBook, int libraryId)
        {
            try
            {
                if (newBook != null)
                {
                    return await bookRepository.AddNewBook(newBook, libraryId);
                }
                else
                {
                    //TODO: Throw an exception
                }
                return false;
            }
            catch (Exception e)
            {
                //TODO: Log Exception in ILogger,File,Application Insights
                return false;
            }
        }

        // To obtain a book object by its bookId
        public async Task<BookDTO> GetBookById(int id)
        {
            try
            {
                if (id != 0)
                {
                    BookDTO bookDTO = await bookRepository.GetBookById(id);
                    return bookDTO;
                }
                else
                {
                    //TODO: Throw an exception
                }
                return null;
            }
            catch (Exception e)
            {
                //TODO: Log Exception in ILogger,File,Application Insights
                return null;
            }
        }

        // To delete a particular book row from the database
        public async Task<bool> DeleteBook(int id)
        {
            try
            {
                if (id != 0)
                {
                    return await bookRepository.DeleteBook(id);
                }
                else
                {
                    //TODO: Throw an exception
                }
                return false;
            }
            catch (Exception e)
            {
                //TODO: Log Exception in ILogger,File,Application Insights
                return false;
            }
        }

        // To update book details in the database
        public async Task<bool> UpdateBook(BookDTO newBook)
        {
            try
            {
                if (newBook != null)
                {
                    return await bookRepository.UpdateBook(newBook);
                }
                else
                {
                    //TODO: Throw an exception
                }
                return false;
            }
            catch (Exception e)
            {
                //TODO: Log Exception in ILogger,File,Application Insights
                return false;
            }
        }

        public async Task<string> UploadImage(BookImageDTO bookImageDTO, string BlobName)
        {
            try
            {
                if (bookImageDTO != null)
                {
                    return await blobRepository.UploadImage(bookImageDTO, BlobName);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                //TODO: Log Exception in ILogger,File,Application Insights
                return null;
            }
        }
    }
}
