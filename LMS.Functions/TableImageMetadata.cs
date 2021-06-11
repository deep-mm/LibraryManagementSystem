using LMS.Functions.Entities;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Threading.Tasks;

namespace LMS.Functions
{
    public static class TableImageMetadata
    {
        [FunctionName("TableImageMetadata")]
        public static async Task Run([BlobTrigger("bookimagecontainer/{genre}/{fileName}", Connection = "AzureWebJobsStorage")] CloudBlockBlob myBlob,
            [Table("Books", Connection = "AzureWebJobsStorage")] IAsyncCollector<Book> bookTable,
            string fileName, ILogger log)
        {
            var metadata = myBlob.Metadata;
            string uri = myBlob.Uri.ToString();

            int bookId = int.Parse(metadata["bookId"]);
            Book book = new Book();
            book.PartitionKey = "bookImages";
            book.RowKey = bookId.ToString();
            book.bookId = bookId;
            book.bookName = metadata["bookName"];
            book.blobUri = uri;

            try
            {
                await bookTable.AddAsync(book);
            }
            catch (Exception e)
            {
                log.LogInformation($"Exception occured: {e.StackTrace}");
            }
            log.LogInformation($"C# Blob trigger function Processed blob\n Id: {book.bookId} \n Name : {book.bookName}");
        }
    }

}
