using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.SharedFiles.DTOs
{
    public class BookImageDTO
    {
        public BookDTO bookDTO { get; set; }
        public byte[] bookImage { get; set; }
        public string fileType { get; set; }
    }
}
