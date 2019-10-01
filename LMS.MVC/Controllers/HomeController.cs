

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
    using Microsoft.Extensions.Configuration;
    using LMS.SharedFiles;
    using LMS.MVC.Helper;
    using Microsoft.Rest.Azure;
    using System.Collections;

    public class HomeController : Controller
    {
        private readonly BookRepository bookRepository;
        private readonly UserRepository userRepository;
        private readonly LibraryRepository libraryRepository;
        private ApplicationInsightsTracking applicationInsightsTracking;
        private string className = "HomeController";

        public HomeController(BookRepository bookRepository, UserRepository userRepository, LibraryRepository libraryRepository)
        {
            this.bookRepository = bookRepository;
            this.userRepository = userRepository;
            this.libraryRepository = libraryRepository;
            applicationInsightsTracking = new ApplicationInsightsTracking();
        }

        public async Task<IActionResult> Index()
        {
            try
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
                        applicationInsightsTracking.TrackEvent("Student: " + user.userName + " Logged In at: " + DateTime.UtcNow);
                        HttpContext.Session.SetString("userEmail", email);
                        return RedirectToAction("Index", "Student");
                    }
                    else if (User.HasClaim(System.Security.Claims.ClaimTypes.Role, "Librarian"))
                    {
                        applicationInsightsTracking.TrackEvent("Librarian: " + user.userName + " Logged In at: " + DateTime.UtcNow);
                        HttpContext.Session.SetString("userEmail", email);
                        return RedirectToAction("Index", "Librarian");
                    }
                    else
                    {
                        HttpContext.Session.SetString("userEmail", "");
                        string searchTerm = HttpContext.Session.GetString("Search");
                        if (searchTerm != null)
                            return View(await bookRepository.GetBooks(searchTerm));
                        else
                            return View(await bookRepository.GetBooks(""));
                    }
                }
                else
                {
                    HttpContext.Session.SetString("userEmail", "");
                    string searchTerm = HttpContext.Session.GetString("Search");
                    if (searchTerm != null)
                        return View(await bookRepository.GetBooks(searchTerm));
                    else
                        return View(await bookRepository.GetBooks(""));
                }
            }
            catch(Exception e)
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
                    throw new Exception(className + "/BookDetails(): book object returned as null from the service layer");
            }
            catch (Exception e)
            {
                applicationInsightsTracking.TrackException(e);
                return RedirectToAction("Error", "Home");
            }
        }

        public IActionResult About()
        {
            try
            {
                ViewData["Message"] = "MS Library";

                return View();
            }
            catch (Exception e)
            {
                applicationInsightsTracking.TrackException(e);
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> ChangeLocation(string locationId)
        {
            try
            {
                if (locationId != null)
                {
                    IEnumerable<LibraryDTO> libraryDTOs = await libraryRepository.GetLibrariesByLocation(int.Parse(locationId)+1);
                    LibraryDTO libraryDTO = libraryDTOs.FirstOrDefault();
                    HttpContext.Session.SetInt32("locationId", int.Parse(locationId));
                    HttpContext.Session.SetInt32("libraryId", libraryDTO.libraryId);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    throw new ArgumentNullException(className + "/ChangeLocation(): locationId received is null");
                }
            }
            catch (Exception e)
            {
                applicationInsightsTracking.TrackException(e);
                return RedirectToAction("Error", "Home");
            }
        }

        public IActionResult Contact()
        {
            try
            {
                ViewData["Message"] = "MS Library Contact Page";

                return View();
            }
            catch (Exception e)
            {
                applicationInsightsTracking.TrackException(e);
                return RedirectToAction("Error", "Home");
            }
        }

        public IActionResult Privacy()
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

        [HttpPost]
        public IActionResult SearchBook(string searchTerm)
        {
            if(searchTerm!=null)
                HttpContext.Session.SetString("Search", searchTerm);
            else
                HttpContext.Session.SetString("Search", "");

            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
