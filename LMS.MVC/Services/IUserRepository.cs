using LMS.SharedFiles.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.MVC.Services
{
    public interface IUserRepository
    {
        Task<UserDTO> GetUserByName(string name);

        Task<bool> AddNewUser(UserDTO user);

        Task<IEnumerable<BookOrdersDTO>> GetBookHistory(int userId);
    }
}
