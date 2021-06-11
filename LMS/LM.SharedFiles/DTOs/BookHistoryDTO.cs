using System;

namespace LMS.SharedFiles.DTOs
{
    public class BookHistoryDTO
    {
        public int userBookAssociationId { get; set; }
        public string BookName { get; set; }

        public string LibraryName { get; set; }

        public DateTime DueDate { get; set; }
    }
}
