using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMS.MVC.Models;
using LMS.MVC.Services;
using LMS.SharedFiles.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace LMS.MVC.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentController : Controller
    {
        private readonly LibraryRepository libraryRepository;
        private readonly BookRepository bookRepository;
        private readonly UserRepository userRepository;

        public StudentController(LibraryRepository libraryRepository, BookRepository bookRepository, UserRepository userRepository)
        {
            this.libraryRepository = libraryRepository;
            this.bookRepository = bookRepository;
            this.userRepository = userRepository;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<BookDTO> allBooks = await libraryRepository.GetAvailaibleBooks();
            return View(allBooks);
        }

        public async Task<IActionResult> BookDetails([FromRoute] int id)
        {
            BookDTO book = await bookRepository.GetBookById(id);
            return View(book);
        }

        public async Task<IActionResult> History()
        {
            string email = HttpContext.Session.GetString("userEmail");
            UserDTO user = await userRepository.GetUserByName(email);
            int userId = user.userId;
            IEnumerable<BookOrdersDTO> bookOrdersDTOs = await userRepository.GetBookHistory(userId);
            return View(bookOrdersDTOs);
        }

        public async Task<IActionResult> Checkout([FromRoute] int id)
        {
            string email = HttpContext.Session.GetString("userEmail");
            //If null add session expired and logout
            UserDTO user = await userRepository.GetUserByName(email);
            int userId = user.userId;
            bool result = await libraryRepository.CheckoutBook(id, userId);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Return([FromRoute] int id)
        {
            string email = HttpContext.Session.GetString("userEmail");
            //If null add session expired and logout
            UserDTO user = await userRepository.GetUserByName(email);
            int userId = user.userId;
            await libraryRepository.ReturnBook(id,userId);
            return RedirectToAction(nameof(Index));
        }

    }
}