

namespace LMS.MVC.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using LMS.MVC.Models;
    using LMS.MVC.Services;
    using LMS.SharedFiles.DTOs;
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;

    public class HomeController : Controller
    {
        private readonly BookRepository bookRepository;
        private readonly UserRepository userRepository;

        public HomeController(BookRepository bookRepository, UserRepository userRepository)
        {
            this.bookRepository = bookRepository;
            this.userRepository = userRepository;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                string email = User.Identity.Name;
                string[] emailObjects = email.Split('#');
                email = emailObjects[emailObjects.Length - 1];
                UserDTO user = await userRepository.GetUserByName(email);
                if (user == null)
                {
                    UserDTO newUser = new UserDTO();
                    newUser.userName = email;
                    newUser.locationId = 1;
                    if (User.HasClaim(System.Security.Claims.ClaimTypes.Role, "Student"))
                        newUser.roleId = 1;
                    else
                        newUser.roleId = 2;

                    HttpContext.Session.SetString("userEmail", email);
                    await userRepository.AddNewUser(newUser);
                }

                if (User.HasClaim(System.Security.Claims.ClaimTypes.Role, "Student"))
                {
                    HttpContext.Session.SetString("userEmail", email);
                    return RedirectToAction("Index", "Student");
                }
                else if (User.HasClaim(System.Security.Claims.ClaimTypes.Role, "Librarian"))
                {
                    HttpContext.Session.SetString("userEmail", email);
                    return RedirectToAction("Index", "Librarian");
                }
                else
                {
                    HttpContext.Session.SetString("userEmail", "");
                    return View(await bookRepository.GetBooks(""));
                }
            }
            else
            {
                HttpContext.Session.SetString("userEmail", "");
                return View(await bookRepository.GetBooks(""));
            }          
        }

        public async Task<IActionResult> BookDetails([FromRoute] int id)
        {
            BookDTO book = await bookRepository.GetBookById(id);
            return View(book);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
