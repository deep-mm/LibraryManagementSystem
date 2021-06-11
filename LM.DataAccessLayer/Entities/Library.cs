using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace LMS.DataAccessLayer.Entities
{
    public class Library
    {
        [Required, Key]
        public int libraryId { get; set; }

        [Required]
        public int locationId { get; set; }

    }
}
