using System.Collections.Generic;
using System.Threading.Tasks;
using LMS.DataAccessLayer.Entities;
using LMS.SharedFiles.DTOs;

namespace LMS.DataAccessLayer.Repositories
{
    public interface IDiscussionRepository
    {
        Task<bool> AddNewPost(PostDTO post);
        Task<IEnumerable<PostDTO>> GetAllPosts();
    }
}