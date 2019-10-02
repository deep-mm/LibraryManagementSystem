using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using LMS.MVC.Helper;
using LMS.MVC.Models;
using LMS.MVC.Services;
using LMS.SharedFiles.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.MVC.Controllers
{
    [Authorize(Roles = "Librarian")]
    public class LibrarianController : Controller
    {
        private readonly LibraryRepository libraryRepository;
        private readonly BookRepository bookRepository;
        private ApplicationInsightsTracking applicationInsightsTracking;
        private string className = "LibrarianController";

        public LibrarianController(LibraryRepository libraryRepository, BookRepository bookRepository)
        {
            this.libraryRepository = libraryRepository;
            this.bookRepository = bookRepository;
            applicationInsightsTracking = new ApplicationInsightsTracking();
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                IEnumerable<BookDTO> books;
                string searchTerm = HttpContext.Session.GetString("Search");

                if (searchTerm != null)
                    books = await bookRepository.GetBooks(searchTerm);
                else
                    books = await bookRepository.GetBooks("");

                if (books != null)
                {
                    return View(books);
                }
                else
                    throw new Exception(className + "/Index(): books array object returned as null from the service layer");
            }
            catch (Exception e)
            {
                applicationInsightsTracking.TrackException(e);
                return RedirectToAction("Error", "Home");
            }
        }

        public IActionResult CreateBook()
        {
            try
            {
                return View();
            }
            catch (Exception e)
            {
                applicationInsightsTracking.TrackException(e);
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost, ActionName("CreateBook")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBook(BookDTO book)
        {
            try
            {
                if (book != null)
                {
                    if (ModelState.IsValid)
                    {
                        bool status = false;
                        var libraryId = HttpContext.Session.GetInt32("libraryId");
                        if (libraryId!=0)
                            status = await bookRepository.AddBook(book, libraryId.Value);
                        else
                            status = await bookRepository.AddBook(book, 2);

                        if (status == true)
                            return RedirectToAction(nameof(Index));
                        else
                            throw new Exception(className + "/CreateBook(): Status returned from the bookRepository layer is false");
                    }
                    return View(book);
                }
                else
                {
                    throw new ArgumentNullException(className + "/CreateBook(): book object parameter is null");
                }
            }
            catch (Exception e)
            {
                applicationInsightsTracking.TrackException(e);
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                if (id == 0)
                {
                    throw new ArgumentNullException(className + "/DeleteBook(): id parameter is null");
                }

                var book = await bookRepository.GetBookById(id);
                if (book == null)
                {
                    throw new NullReferenceException(className + "/DeleteBook(): book object returned as null from the service layer");
                }

                return View(book);
            }
            catch (Exception e)
            {
                applicationInsightsTracking.TrackException(e);
                return RedirectToAction("Error", "Home");
            }
        }

        // POST: Books/Delete/5
  
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                if (id != 0)
                {
                    bool status = await bookRepository.DeleteBook(id);
                    if (status == true)
                        return RedirectToAction(nameof(Index));
                    else
                        throw new Exception(className + "/DeleteConfirmed(): Status returned from the bookRepository layer is false");
                }
                else
                {
                    throw new ArgumentNullException(className + "/DeleteConfirmed(): id parameter is null");
                }
            }
            catch (Exception e)
            {
                applicationInsightsTracking.TrackException(e);
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> BookDetails([FromRoute] int id)
        {
            try
            {
                BookDTO book = await bookRepository.GetBookById(id);
                if (book != null)
                    return View(book);
                else
                    throw new NullReferenceException(className + "/BookDetails(): book object returned as null from the service layer");
            }
            catch (Exception e)
            {
                applicationInsightsTracking.TrackException(e);
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> EditBook(int id)
        {
            try
            {
                if (id == 0)
                {
                    throw new ArgumentNullException(className + "/EditBook(): id oject paramter is null");
                }

                var book = await bookRepository.GetBookById(id);
                BookFormDTO bookFormDTO = new BookFormDTO();
                bookFormDTO.bookDTO = book ?? throw new Exception(className + "/EditBook(): book object returned as null from the service layer");
                return View(bookFormDTO);
            }
            catch (Exception e)
            {
                applicationInsightsTracking.TrackException(e);
                return RedirectToAction("Error", "Home");
            }
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBook(BookFormDTO book)
        {
            try
            {
                if (book != null)
                {
                    if (ModelState.IsValid)
                    {
                        BookImageDTO bookImageDTO = new BookImageDTO();
                        bookImageDTO.bookDTO = book.bookDTO;

                        if (book.bookImage != null)
                        {
                            MemoryStream memoryStream = new MemoryStream();
                            book.bookImage.CopyTo(memoryStream);
                            bookImageDTO.bookImage = memoryStream.ToArray();

                            bookImageDTO.fileType = ".png";
                            string uri = await bookRepository.UploadImage(bookImageDTO);
                            if (uri != null)
                            {
                                book.bookDTO.imageUrl = uri.Replace("\"", "");
                            }
                            else
                            {
                                throw new NullReferenceException(className + "/EditBook(): UploadImage function of bookRepository return null uri");
                            }
                        }

                        bool status = await bookRepository.EditBook(book.bookDTO);
                        if(status==true)
                            return RedirectToAction(nameof(Index));
                        else
                            throw new Exception(className + "/EditBook(): Status returned from the bookRepository layer is false");
                    }
                    return View(book);
                }
                else
                {
                    throw new ArgumentNullException(className + "/EditBook(): book oject paramter is null");
                }
            }
            catch (Exception e)
            {
                applicationInsightsTracking.TrackException(e);
                return RedirectToAction("Error", "Home");
            }
        }
    }

}