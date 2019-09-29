using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.DataAccessLayer.Entities
{
    public class BookLibraryAssociation
    {
        [Required, Key]
        public int bookLibraryAssociationId { get; set; }

        [Required]
        public int bookId { get; set; }

        [Required]
        public int libraryId { get; set; }

        public bool isAvailable { get; set; }

        public bool isCheckedOut { get; set; }

    }
}
