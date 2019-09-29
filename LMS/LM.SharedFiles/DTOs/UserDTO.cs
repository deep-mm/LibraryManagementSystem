using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.SharedFiles.DTOs
{
    public class UserDTO
    {
        public int userId { get; set; }

        public string userName { get; set; }

        public int roleId { get; set; }

        public int locationId { get; set; }
    }
}
