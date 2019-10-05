using System.Collections.Generic;
using System.Threading.Tasks;
using LMS.SharedFiles.DTOs;

namespace LMS.DataAccessLayer.Repositories
{
    public interface IAzureSearchService
    {
        Task<IEnumerable<PostDTO>> Search(string searchValue);
    }
}