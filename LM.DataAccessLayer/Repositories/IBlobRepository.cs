using LMS.SharedFiles.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DataAccessLayer.Repositories
{
    public interface IBlobRepository
    {
        Task<string> UploadImage(BookImageDTO bookImageDTO, string blobName);
    }
}
