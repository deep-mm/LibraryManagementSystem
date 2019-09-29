using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace LMS.SharedFiles.DTOs
{
    public class BookFormDTO
    {
        public BookDTO bookDTO { get; set; }
        public IFormFile bookImage { get; set; }
    }
}
