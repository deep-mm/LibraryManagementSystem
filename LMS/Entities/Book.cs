using LMS.SharedFiles.DTOs;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace LMS.DataAccessLayer.Entities
{
    public class Book
    {
        [Required, Key]
        public int bookId { get; set; }

        [Required, StringLength(200)]
        public string title { get; set; }

        [Required, StringLength(100)]
        public string author { get; set; }

        public double price { get; set; }

        public string imageUrl { get; set; }

        [Required]
        public GenreTypes genre { get; set; }
    }
}
