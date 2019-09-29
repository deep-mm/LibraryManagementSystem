using System.ComponentModel.DataAnnotations;

namespace LMS.DataAccessLayer.Entities
{
    public class Location
    {
        [Required, Key]
        public int locationId { get; set; }

        [Required, StringLengthAttribute(200)]
        public string locationName { get; set; }
    }
}
