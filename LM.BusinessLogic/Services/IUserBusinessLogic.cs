using LMS.SharedFiles.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS.BusinessLogic.Services
{
    public interface IUserBusinessLogic
    {
        Task<bool> AddNewUser(UserDTO userDTO);
        Task<UserDTO> GetUserById(int userId);
        Task<UserDTO> GetUserByName(string username);
        Task<IEnumerable<BookOrdersDTO>> UserOrderHistory(int userId);
    }
}