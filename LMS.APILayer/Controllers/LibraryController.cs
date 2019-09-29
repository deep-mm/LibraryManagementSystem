using AutoMapper;
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
    [Authorize]
    [Route("api/library")]
    [ApiController]
    public class LibraryController : Controller
    {
        private readonly LibraryBusinessLogic libraryBusinessLogic;

        public LibraryController(ReadDBContext readDBContext, IMapper mapper)
        {
            LibraryRepository libraryRepository = new LibraryRepository(readDBContext, mapper);
            this.libraryBusinessLogic = new LibraryBusinessLogic(libraryRepository);
        }

        public IConfiguration Configuration { get; }

        // GET: api/library/availaibleBooks
        [HttpGet("availaibleBooks")]
        public async Task<IActionResult> GetAllAvailaibleBooks()
        {
            try
            {
                IEnumerable<BookDTO> availaibleBooks = await libraryBusinessLogic.GetAllAvailaibleBooks();
                return Ok(availaibleBooks);
            }
            catch (Exception e)
            {
                //TODO: Log Exception in ILogger,File,Application Insights
                return BadRequest("Error occured while retreiving books from database");
            }
        }

        // GET: api/Books/checkout/5/1
        [HttpGet("checkout/{bookId}/{userId}")]
        public async Task<IActionResult> CheckoutBook([FromRoute] int bookId, [FromRoute] int userId)
        {
            try
            {
                bool status = await libraryBusinessLogic.CheckoutBook(bookId, userId);
                if (status == true)
                    return Ok();
                else
                    return BadRequest("Error occured while checking out the book");
            }
            catch (Exception e)
            {
                //TODO: Log Exception in ILogger,File,Application Insights
                return BadRequest("Error occured while checking out the book");
            }
        }


        // GET: api/Books/return/5/1
        [HttpGet("return/{bookId}/{userId}")]
        public async Task<IActionResult> ReturnBook([FromRoute] int bookId, [FromRoute] int userId)
        {
            try
            {
                bool status = await libraryBusinessLogic.ReturnBook(bookId, userId);
                if (status == true)
                    return Ok();
                else
                    return BadRequest("Error occured while returning the book");
            }
            catch (Exception e)
            {
                //TODO: Log Exception in ILogger,File,Application Insights
                return BadRequest("Error occured while returning the book");
            }
        }
    }
}