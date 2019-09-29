using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using LMS.BusinessLogic.Services;
using LMS.DataAccessLayer.DatabaseContext;
using LMS.DataAccessLayer.Repositories;
using LMS.SharedFiles.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace LMS.APILayer.Controllers
{
    [Authorize]
    [Route("api/users")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UserBusinessLogic userBusinessLogic;

        public UserController(ReadDBContext readDBContext, IMapper mapper)
        {
            UserRepository userRepository = new UserRepository(readDBContext, mapper);
            this.userBusinessLogic = new UserBusinessLogic(userRepository);
        }

        // GET: api/users/id/1
        [HttpGet("id/{userId}")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            try
            {
                UserDTO user = await userBusinessLogic.GetUserById(userId);
                return Ok(user);
            }
            catch (Exception e)
            {
                //TODO: Log Exception in ILogger,File,Application Insights
                return BadRequest("Error occured while retreiving books from database");
            }
        }

        // GET: api/users/name/deep
        [HttpGet("name/{username}")]
        public async Task<IActionResult> GetUserByName(string username)
        {
            try
            {
                UserDTO user = await userBusinessLogic.GetUserByName(Base64UrlEncoder.Decode(username));
                return Ok(user);
            }
            catch (Exception e)
            {
                //TODO: Log Exception in ILogger,File,Application Insights
                return BadRequest("Error occured while retreiving books from database");
            }
        }

        // GET: api/users/history/1
        [HttpGet("history/{userId}")]
        public async Task<IActionResult> GetBookOrders(int userId)
        {
            try
            {
                IEnumerable<BookOrdersDTO> bookOrdersDTOs = await userBusinessLogic.UserOrderHistory(userId);
                return Ok(bookOrdersDTOs);
            }
            catch (Exception e)
            {
                //TODO: Log Exception in ILogger,File,Application Insights
                return BadRequest("Error occured while retreiving books from database");
            }
        }

        // POST: api/users/addUser
        [HttpPost("addUser")]
        public async Task<IActionResult> AddNewUser([FromBody] UserDTO user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                bool status = await userBusinessLogic.AddNewUser(user);
                if (status == true)
                    return Ok(HttpStatusCode.Created);
                else
                    return BadRequest("Error occured while adding a new book");
            }
            catch (Exception e)
            {
                //TODO: Log Exception in ILogger,File,Application Insights
                return BadRequest("Error occured while retreiving books from database");
            }
        }

    }
}