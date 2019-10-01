using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMS.MVC.Helper;
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
        private ApplicationInsightsTracking applicationInsightsTracking;
        private string className = "StudentController";

        public StudentController(LibraryRepository libraryRepository, BookRepository bookRepository, UserRepository userRepository)
        {
            this.libraryRepository = libraryRepository;
            this.bookRepository = bookRepository;
            this.userRepository = userRepository;
            applicationInsightsTracking = new ApplicationInsightsTracking();
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                IEnumerable<BookDTO> allBooks;
                string searchTerm = HttpContext.Session.GetString("Search");
                if (searchTerm != null)
                    allBooks = await libraryRepository.GetAvailaibleBooks(searchTerm);
                else
                    allBooks = await libraryRepository.GetAvailaibleBooks("");


                if (allBooks != null)
                    return View(allBooks);
                else
                    throw new NullReferenceException(className + "/Index(): allBooks array returned as null from libraryRepository");
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
                if (id != 0) {
                    BookDTO book = await bookRepository.GetBookById(id);
                    return View(book);
                }
                else
                {
                    throw new ArgumentNullException(className + "/BookDetails(): id parameter is null");
                }
            }
            catch (Exception e)
            {
                applicationInsightsTracking.TrackException(e);
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> History()
        {
            try
            {
                string email = HttpContext.Session.GetString("userEmail");
                IEnumerable<LocationDTO> locations = await libraryRepository.GetAllLocations();
                if (email != null)
                {
                    UserDTO user = await userRepository.GetUserByName(email);
                    if (user != null)
                    {
                        int userId = user.userId;
                        IEnumerable<BookOrdersDTO> bookOrdersDTOs = await userRepository.GetBookHistory(userId);
                        if (bookOrdersDTOs != null)
                        {
                            IEnumerable<BookHistoryDTO> bookHistoryDTOs = new List<BookHistoryDTO>();
                            foreach (var book in bookOrdersDTOs)
                            {
                                BookHistoryDTO bookHistoryDTO = new BookHistoryDTO();
                                bookHistoryDTO.BookName = book.BookName;
                                bookHistoryDTO.userBookAssociationId = book.userBookAssociationId;
                                bookHistoryDTO.DueDate = book.DueDate;
                                bookHistoryDTO.LibraryName = locations.Where(l => l.locationId == (book.LibraryId-1)).FirstOrDefault().locationName;
                                bookHistoryDTOs = bookHistoryDTOs.Concat(new[] { bookHistoryDTO });
                            }
                            return View(bookHistoryDTOs);
                        }
                        else
                            throw new NullReferenceException(className + "/History(): bookOrderDTOs object array returned as null from userRepository");
                    }
                    else
                    {
                        throw new NullReferenceException(className + "/History(): user object returned as null from userRepository");
                    }
                }
                else
                {
                    return RedirectToAction("SignOut", "Account");
                }
            }
            catch (Exception e)
            {
                applicationInsightsTracking.TrackException(e);
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> Checkout([FromRoute] int id)
        {
            try
            {
                if (id != 0)
                {
                    string email = HttpContext.Session.GetString("userEmail");
                    if (email != null)
                    {
                        UserDTO user = await userRepository.GetUserByName(email);
                        if (user != null)
                        {
                            int userId = user.userId;
                            bool result = await libraryRepository.CheckoutBook(id, userId);
                            if (result == true)
                                return RedirectToAction(nameof(Index));
                            else
                                throw new Exception(className + "/Checkout(): Result returned as false from the libraryRepository");
                        }
                        else
                        {
                            throw new NullReferenceException(className + "/Checkout(): user object returned as null from userRepository");
                        }
                    }
                    else
                    {
                        return RedirectToAction("SignOut", "Account");
                    }
                }
                else
                {
                    throw new ArgumentNullException(className + "/Checkout(): id parameter is null");
                }
            }
            catch (Exception e)
            {
                applicationInsightsTracking.TrackException(e);
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> Return([FromRoute] int id)
        {
            try
            {
                if (id != 0)
                {
                    string email = HttpContext.Session.GetString("userEmail");
                    if (email != null)
                    {
                        UserDTO user = await userRepository.GetUserByName(email);
                        if (user != null)
                        {
                            int userId = user.userId;
                            bool result = await libraryRepository.ReturnBook(id, userId);
                            if(result==true)
                                return RedirectToAction(nameof(Index));
                            else
                                throw new Exception(className + "/Return(): Result returned as false from the libraryRepository");
                        }
                        else
                        {
                            throw new NullReferenceException(className + "/Return(): user object returned as null from userRepository");
                        }
                    }
                    else
                    {
                        return RedirectToAction("SignOut", "Account");
                    }
                }
                else
                {
                    throw new ArgumentNullException(className + "/Return(): id parameter is null");
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