/*
 * This contains definition of all the functions performed to get details of a user or to add a new user
 * Author: Deep Mehta
 */
namespace LMS.DataAccessLayer.Repositories
{
    using LMS.DataAccessLayer.Entities;
    using LMS.SharedFiles.DTOs;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public interface IUserRepository
    {
        // Get a user by its ID
        Task<UserDTO> GetUserById(int userId);

        // Get a user by its Username
        Task<UserDTO> GetUserByName(string username);

        //Add a new user entry to the database
        Task<bool> AddNewUser(UserDTO userDTO);

        //Get all books checkedout by a user
        Task<IEnumerable<BookOrdersDTO>> UserOrderHistory(int userId);
    }
}
