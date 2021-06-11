using LMS.SharedFiles.DTOs;
using System.Threading.Tasks;

namespace LMS.DataAccessLayer.Repositories
{
    public interface IBlobRepository
    {
        Task<string> UploadImage(BookImageDTO bookImageDTO, string blobName);
    }
}
