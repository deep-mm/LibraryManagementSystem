namespace LMS.Functions.Entities
{
    public class Book
    {
        public string PartitionKey { get; set; }

        public string RowKey { get; set; }

        public int bookId { get; set; }

        public string bookName { get; set; }

        public string blobUri { get; set; }
    }
}
