using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.SharedFiles.DTOs
{
    public class BookOrdersDTO
    {

        public int userBookAssociationId { get; set; }
        public string BookName { get; set; }

        public int LibraryId { get; set; }

        public DateTime DueDate { get; set; }
    }
}
