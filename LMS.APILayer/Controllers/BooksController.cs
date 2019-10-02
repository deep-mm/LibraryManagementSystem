using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using LMS.APILayer.Services;
using LMS.BusinessLogic.Services;
using LMS.DataAccessLayer.DatabaseContext;
using LMS.DataAccessLayer.Repositories;
using LMS.SharedFiles.DTOs;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;

namespace LMS.APILayer.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : Controller
    {
        private readonly IBooksBusinessLogic booksBusinessLogic;
        private ApplicationInsightsTracking applicationInsightsTracking;
        private string className = "BooksController";

        public IConfiguration Configuration { get; }


        public BooksController(IBooksBusinessLogic booksBusinessLogic, IConfiguration configuration)
        {
            this.booksBusinessLogic = booksBusinessLogic;
            Configuration = configuration;
            applicationInsightsTracking = new ApplicationInsightsTracking();
        }

        //GET: api/books
        [HttpGet("{libraryId}")]
        public async Task<IActionResult> GetAllBooks([FromRoute] string libraryId)
        {
            try
            {
                IEnumerable<BookDTO> books = await booksBusinessLogic.GetBookByName("",int.Parse(libraryId));
                if (books != null)
                {
                    return Ok(books);
                }
                else
                {
                    throw new Exception(className + "/GetAllBooks(): Books list returned as null from DataAccessLayer");
                }
            }
            catch(Exception e)
            {
                applicationInsightsTracking.TrackException(e);
                return BadRequest("Error occured while retreiving books from database");
            }
        }

        //GET: api/books/name/harry
        [HttpGet("name/{search}/{libraryId}")]
        public async Task<IActionResult> GetBookByName([FromRoute] string search,[FromRoute] string libraryId)
        {
            try
            {
                IEnumerable<BookDTO> books = await booksBusinessLogic.GetBookByName(search,int.Parse(libraryId));
                if (books != null)
                {
                    return Ok(books);
                }
                else
                {
                    throw new Exception(className + "/GetBookByName(): Books list returned as null from DataAccessLayer");
                }
            }
            catch (Exception e)
            {
                applicationInsightsTracking.TrackException(e);
                return BadRequest("Error occured while retreiving books from database");
            }
        }


        // GET: api/books/id/5
        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetBookById([FromRoute] int id)
        {
            try
            {
                BookDTO book = await booksBusinessLogic.GetBookById(id);
                if (book != null)
                    return Ok(book);

                else
                    throw new Exception(className + "/GetBookById(): Book returned as null from DataAccessLayer");
            }
            catch (Exception e)
            {
                applicationInsightsTracking.TrackException(e);
                return BadRequest("Error occured while retreiving books from database");
            }
        }

        [Authorize]
        // POST: api/books/addBook/5
        [HttpPost("addBook/{libraryId}")]
        public async Task<IActionResult> AddNewBook([FromBody] BookDTO book, [FromRoute] int libraryId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                bool status = await booksBusinessLogic.AddNewBook(book, libraryId);

                if (status == true)
                    return Ok(HttpStatusCode.Created);
                else
                    throw new Exception(className + "/AddNewBook(): Status returned as false from DataAccessLayer");
            }
            catch (Exception e)
            {
                applicationInsightsTracking.TrackException(e);
                return BadRequest("Error occured while adding a new book");
            }
        }

        [Authorize]
        //PUT api/books/updateBook
        [HttpPut("updateBook")]
        public async Task<IActionResult> PutBook([FromBody] BookDTO book)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                bool status = await booksBusinessLogic.UpdateBook(book);
                if (status == true)
                    return Ok(HttpStatusCode.Created);
                else
                    throw new Exception(className + "/PutBook(): Status returned as false from DataAccessLayer");
            }
            catch (Exception e)
            {
                applicationInsightsTracking.TrackException(e);
                return BadRequest("Error occured while updating the book");
            }
        }

        [Authorize]
        // DELETE: api/Books/delete/5
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteBook([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                bool status = await booksBusinessLogic.DeleteBook(id);
                if (status == true)
                    return Ok();
                else
                    throw new Exception(className + "/DeleteBook(): Status returned as false from DataAccessLayer");
            }
            catch (Exception e)
            {
                applicationInsightsTracking.TrackException(e);
                return BadRequest("Error occured while deleting the book");
            }
        }

        [Authorize]
        [HttpPost("uploadBookImage")]
        public async Task<IActionResult> UploadPhotoAsync([FromBody] BookImageDTO bookImageDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                string uri = await booksBusinessLogic.UploadImage(bookImageDTO, "bookimagecontainer");
                
                if (uri!=null)
                    return Ok(uri);
                else
                    throw new Exception(className + "/UploadPhotoAsync(): uri returned as null from DataAccessLayer");
            }
            catch (Exception e)
            {
                applicationInsightsTracking.TrackException(e);
                return BadRequest("Error occured while uploading image");
            }
            
        }

    }
}