using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.SharedFiles.DTOs
{
    public class BookDTO
    {
        public int bookId { get; set; }

        public string title { get; set; }

        public string author { get; set; }

        public double price { get; set; }

        public GenreTypes genre { get; set; }

        public string imageUrl { get; set; }
    }
}
