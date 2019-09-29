using System.ComponentModel.DataAnnotations;

namespace LMS.DataAccessLayer.Entities
{
    public class Role
    {
        [Required, Key]
        public int roleId { get; set; }

        [Required, StringLength(200)]
        public string name { get; set; }
    }
}
