using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;

namespace LMS.DataAccessLayer.Entities
{
    public class UserBookAssociation
    {
        [Required, Key]
        public int id { get; set; }

        [Required]
        public int userId { get; set; }

        [Required]
        public int BookLibraryAssociationId { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

    }
}
