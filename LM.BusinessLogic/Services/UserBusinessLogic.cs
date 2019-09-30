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
            return await userRepository.GetUserById(userId);
        }

        // Get a user by its Username
        public async Task<UserDTO> GetUserByName(string username)
        {
            return await userRepository.GetUserByName(username);
        }

        //Add a new user entry to the database
        public async Task<bool> AddNewUser(UserDTO userDTO)
        {
            return await userRepository.AddNewUser(userDTO);
        }

        //Get all books checkedout by a user
        public async Task<IEnumerable<BookOrdersDTO>> UserOrderHistory(int userId)
        {
            return await userRepository.UserOrderHistory(userId);
        }
    }
}
