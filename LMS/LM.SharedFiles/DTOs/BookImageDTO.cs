namespace LMS.SharedFiles.DTOs
{
    public class BookImageDTO
    {
        public BookDTO bookDTO { get; set; }
        public byte[] bookImage { get; set; }
        public string fileType { get; set; }
    }
}
