using LMS.SharedFiles.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS.DataAccessLayer.Repositories
{
    public interface IDiscussionRepository
    {
        Task<bool> AddNewPost(PostDTO post);
        Task<IEnumerable<PostDTO>> GetAllPosts();
    }
}