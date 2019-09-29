using System;
using System.IO;
using System.Threading.Tasks;
using LMS.Functions.Entities;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Table;

namespace LMS.Functions
{
    public static class TableImageMetadata
    {
        [FunctionName("TableImageMetadata")]
        public static async Task Run([BlobTrigger("bookimagecontainer/{genre}/{fileName}", Connection = "AzureWebJobsStorage")]CloudBlockBlob myBlob,
            [Table("Books", Connection = "AzureWebJobsStorage")] IAsyncCollector<Book> bookTable,
            string fileName, ILogger log)
        {
            var metadata = myBlob.Metadata;
            string uri = myBlob.Uri.ToString();

            int bookId = int.Parse(metadata["bookId"]);
            Book book = new Book();
            book.PartitionKey = metadata["genre"];
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

            }
            log.LogInformation($"C# Blob trigger function Processed blob\n Id: {book.bookId} \n Name : {book.bookName}");
        }
    }

}
