using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using LMS.MVC.Models;
using LMS.MVC.Services;
using LMS.SharedFiles.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.MVC.Controllers
{
    [Authorize(Roles = "Librarian")]
    public class LibrarianController : Controller
    {
        private readonly LibraryRepository libraryRepository;
        private readonly BookRepository bookRepository;

        public LibrarianController(LibraryRepository libraryRepository, BookRepository bookRepository)
        {
            this.libraryRepository = libraryRepository;
            this.bookRepository = bookRepository;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<BookDTO> books = await bookRepository.GetBooks("");
            return View(books);
        }

        public IActionResult CreateBook()
        {
            return View();
        }

        [HttpPost, ActionName("CreateBook")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBook(BookDTO book)
        {
            if (ModelState.IsValid)
            {
                await bookRepository.AddBook(book,2);
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> DeleteBook(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await bookRepository.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
  
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await bookRepository.DeleteBook(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> BookDetails([FromRoute] int id)
        {
            BookDTO book = await bookRepository.GetBookById(id);
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> EditBook(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await bookRepository.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            BookFormDTO bookFormDTO = new BookFormDTO();
            bookFormDTO.bookDTO = book;
            return View(bookFormDTO);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBook(BookFormDTO book)
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
                    book.bookDTO.imageUrl = uri.Replace("\"","");
                }

                await bookRepository.EditBook(book.bookDTO);
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }
    }

}