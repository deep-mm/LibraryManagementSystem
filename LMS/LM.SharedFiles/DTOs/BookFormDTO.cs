using Microsoft.AspNetCore.Http;

namespace LMS.SharedFiles.DTOs
{
    public class BookFormDTO
    {
        public BookDTO bookDTO { get; set; }
        public IFormFile bookImage { get; set; }
    }
}
