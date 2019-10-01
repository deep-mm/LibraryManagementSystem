using AutoMapper;
using LMS.APILayer.Services;
using LMS.BusinessLogic.Services;
using LMS.DataAccessLayer.DatabaseContext;
using LMS.DataAccessLayer.Repositories;
using LMS.SharedFiles.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LMS.APILayer.Controllers
{
    [Route("api/library")]
    [ApiController]
    public class LibraryController : Controller
    {
        private readonly LibraryBusinessLogic libraryBusinessLogic;
        private ApplicationInsightsTracking applicationInsightsTracking;
        private string className = "LibraryController";

        public LibraryController(ReadDBContext readDBContext, IMapper mapper)
        {
            LibraryRepository libraryRepository = new LibraryRepository(readDBContext, mapper);
            this.libraryBusinessLogic = new LibraryBusinessLogic(libraryRepository);
            applicationInsightsTracking = new ApplicationInsightsTracking();
        }

        public IConfiguration Configuration { get; }

        [Authorize]
        // GET: api/library/availaibleBooks/Harry
        [HttpGet("availaibleBooks/{bookName}")]
        public async Task<IActionResult> GetAllAvailaibleBooks([FromRoute] string bookName)
        {
            try
            {
                IEnumerable<BookDTO> availaibleBooks;
                if (bookName==null)
                    availaibleBooks = await libraryBusinessLogic.GetAllAvailaibleBooks(null);
                else
                    availaibleBooks = await libraryBusinessLogic.GetAllAvailaibleBooks(bookName);

                if (availaibleBooks != null)
                    return Ok(availaibleBooks);

                else
                    throw new Exception(className + "/GetAllAvailaibleBooks(): AvailaibleBooks returned as null from the database");
            }
            catch (Exception e)
            {
                applicationInsightsTracking.TrackException(e);
                return BadRequest("Error occured while retreiving books from database");
            }
        }

        [Authorize]
        // GET: api/library/availaibleBooks/Harry
        [HttpGet("availaibleBooks")]
        public async Task<IActionResult> GetAllAvailaibleBooks()
        {
            try
            {
                IEnumerable<BookDTO> availaibleBooks = availaibleBooks = await libraryBusinessLogic.GetAllAvailaibleBooks(null);

                if (availaibleBooks != null)
                    return Ok(availaibleBooks);

                else
                    throw new Exception(className + "/GetAllAvailaibleBooks(): AvailaibleBooks returned as null from the database");
            }
            catch (Exception e)
            {
                applicationInsightsTracking.TrackException(e);
                return BadRequest("Error occured while retreiving books from database");
            }
        }

        [Authorize]
        // GET: api/library/checkout/5/1
        [HttpGet("checkout/{bookId}/{userId}")]
        public async Task<IActionResult> CheckoutBook([FromRoute] int bookId, [FromRoute] int userId)
        {
            try
            {
                bool status = await libraryBusinessLogic.CheckoutBook(bookId, userId);
                if (status == true)
                    return Ok();
                else
                    throw new Exception(className + "/CheckoutBook(): Status returned as false from the database");
            }
            catch (Exception e)
            {
                applicationInsightsTracking.TrackException(e);
                return BadRequest("Error occured while checking out the book");
            }
        }


        [Authorize]
        // GET: api/library/return/5/1
        [HttpGet("return/{bookId}/{userId}")]
        public async Task<IActionResult> ReturnBook([FromRoute] int bookId, [FromRoute] int userId)
        {
            try
            {
                bool status = await libraryBusinessLogic.ReturnBook(bookId, userId);
                if (status == true)
                    return Ok();
                else
                    throw new Exception(className + "/ReturnBook(): Status returned as false from the database");
            }
            catch (Exception e)
            {
                applicationInsightsTracking.TrackException(e);
                return BadRequest("Error occured while returning the book");
            }
        }

        // GET: api/library/allLocations
        [HttpGet("allLocations")]
        public async Task<IActionResult> GetAllLocations()
        {
            try
            {
                IEnumerable<LocationDTO> locationDTOs = await libraryBusinessLogic.GetAllLocations();
                if (locationDTOs != null)
                    return Ok(locationDTOs);
                else
                    throw new Exception(className + "/GetAllLocations(): locationDTO array returned as null from the DataAccessLayer");
            }
            catch (Exception e)
            {
                applicationInsightsTracking.TrackException(e);
                return BadRequest("Error occured while getting all locations from the database");
            }
        }

        // GET: api/library/getLibrariesByLocation/"2"
        [HttpGet("getLibrariesByLocation/{locationId}")]
        public async Task<IActionResult> GetLibrariesByLocation([FromRoute] string locationId)
        {
            try
            {
                if (locationId != null)
                {
                    IEnumerable<LibraryDTO> libraryDTOs = await libraryBusinessLogic.GetLibrariesByLocation(int.Parse(locationId));
                    if (libraryDTOs != null)
                        return Ok(libraryDTOs);
                    else
                        throw new Exception(className + "/GetLibrariesByLocation(): locationDTO array returned as null from the DataAccessLayer");
                }
                else
                {
                    throw new ArgumentNullException(className + "/GetLibrariesByLocation(): locationId paramter received is null");
                }
            }
            catch (Exception e)
            {
                applicationInsightsTracking.TrackException(e);
                return BadRequest("Error occured while getting all locations from the database");
            }
        }
    }
}