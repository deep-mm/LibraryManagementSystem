using LMS.DataAccessLayer.Repositories;
using LMS.SharedFiles.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.BusinessLogic.Services
{
    public class UserBusinessLogic
    {
        private readonly IUserRepository userRepository;

        public UserBusinessLogic(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<UserDTO> GetUserById(int userId)
        {
            try
            {
                if(userId != 0)
                {
                    return await userRepository.GetUserById(userId);
                }
                else
                {
                    //throw a new exception
                }
                return null;
            }
            catch (Exception e)
            {
                //TODO: Log Exception in ILogger,File,Application Insights
                return null;
            }
        }

        // Get a user by its Username
        public async Task<UserDTO> GetUserByName(string username)
        {
            try
            {
                if (username != null)
                {
                    return await userRepository.GetUserByName(username);
                }
                else
                {
                    //throw a new exception
                }
                return null;
            }
            catch (Exception e)
            {
                //TODO: Log Exception in ILogger,File,Application Insights
                return null;
            }
        }

        //Add a new user entry to the database
        public async Task<bool> AddNewUser(UserDTO userDTO)
        {
            try
            {
                if (userDTO != null)
                {
                    return await userRepository.AddNewUser(userDTO);
                }
                else
                {
                    //throw a new exception
                }
                return false;
            }
            catch (Exception e)
            {
                //TODO: Log Exception in ILogger,File,Application Insights
                return false;
            }
        }

            //Get all books checkedout by a user
        public async Task<IEnumerable<BookOrdersDTO>> UserOrderHistory(int userId)
        {
            try
            {
                if (userId != 0)
                {
                    return await userRepository.UserOrderHistory(userId);
                }
                else
                {
                    //throw a new exception
                }
                return null;
            }
            catch (Exception e)
            {
                //TODO: Log Exception in ILogger,File,Application Insights
                return null;
            }
        }
    }
}
