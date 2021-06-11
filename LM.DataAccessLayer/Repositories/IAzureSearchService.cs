using LMS.SharedFiles.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS.DataAccessLayer.Repositories
{
    public interface IAzureSearchService
    {
        Task<IEnumerable<PostDTO>> Search(string searchValue);
    }
}