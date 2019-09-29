using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.DataAccessLayer.Entities
{
    public class User
    {
        [Required, Key]
        public int userId { get; set; }

        [Required]
        public string userName { get; set; }

        public string password { get; set; }

        [Required]
        public int roleId { get; set; }

        [Required]
        public int locationId { get; set; }
    }
}
