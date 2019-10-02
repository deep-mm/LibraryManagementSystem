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
        private readonly IUserBusinessLogic userBusinessLogic;
        private ApplicationInsightsTracking applicationInsightsTracking;
        private string className = "UserController";

        public UserController(IUserBusinessLogic userBusinessLogic)
        {
            applicationInsightsTracking = new ApplicationInsightsTracking();
            this.userBusinessLogic = userBusinessLogic;
        }

        // GET: api/users/id/1
        [HttpGet("id/{userId}")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            try
            {
                UserDTO user = await userBusinessLogic.GetUserById(userId);
                if(user!=null)
                    return Ok(user);
                else
                    throw new Exception(className + "/GetUserById(): user object returned as null from DataAccessLayer");
            }
            catch (Exception e)
            {
                applicationInsightsTracking.TrackException(e);
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
                if (user != null)
                    return Ok(user);
                else
                    throw new Exception(className + "/GetUserByName(): user object returned as null from DataAccessLayer");
            }
            catch (Exception e)
            {
                applicationInsightsTracking.TrackException(e);
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
                if (bookOrdersDTOs != null)
                {
                    return Ok(bookOrdersDTOs);
                }
                else
                {
                    throw new Exception(className + "/GetBookOrders(): bookOrdersDTOs object returned as null from DataAccessLayer");
                }
            }
            catch (Exception e)
            {
                applicationInsightsTracking.TrackException(e);
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
                    throw new Exception(className + "/AddNewUser(): status returned as false from DataAccessLayer");
            }
            catch (Exception e)
            {
                applicationInsightsTracking.TrackException(e);
                return BadRequest("Error occured while retreiving books from database");
            }
        }

    }
}