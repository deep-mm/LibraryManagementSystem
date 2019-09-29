using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LMS.DataAccessLayer.DatabaseContext;
using LMS.SharedFiles.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace LMS.DataAccessLayer.Repositories
{
    public class BlobRepository : IBlobRepository
    {
        private readonly ReadDBContext readDBContext;
        private readonly IMapper mapper;
        private readonly ICloudBlob cloudBlob;
        private CloudBlobClient cloudBlobClient;

        public BlobRepository(ReadDBContext readDBContext, IMapper mapper, IConfiguration configuration)
        {
            this.readDBContext = readDBContext;
            this.mapper = mapper;
            Configuration = configuration;
            InitializeBlob();
        }

        public IConfiguration Configuration { get; }

        public void InitializeBlob()
        {
            try
            {
                string azureStorageConnectionString = Configuration["AzureStorageConnectionString"];
                CloudStorageAccount storageAccount;
                if (CloudStorageAccount.TryParse(azureStorageConnectionString, out storageAccount))
                {
                    cloudBlobClient = storageAccount.CreateCloudBlobClient();
                }
                else
                {
                    Console.WriteLine("Connection String is invalid");
                }
            }
            catch(Exception e)
            {

            }
        }

        public async Task<string> UploadImage(BookImageDTO bookImageDTO, string blobName)
        {
            try
            {
                string genre = bookImageDTO.bookDTO.genre.ToString();
                CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(blobName);
                await cloudBlobContainer.CreateIfNotExistsAsync();

                BookDTO book = bookImageDTO.bookDTO;
                CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(genre + "/" + book.bookId.ToString() + ".png");
                cloudBlockBlob.Metadata.Add("bookId", book.bookId.ToString());
                cloudBlockBlob.Metadata.Add("bookName", book.title);
                cloudBlockBlob.Metadata.Add("genre", book.genre.ToString());
                await cloudBlockBlob.UploadFromByteArrayAsync(bookImageDTO.bookImage, 0, bookImageDTO.bookImage.Length - 1);
                string uri = cloudBlockBlob.Uri.ToString();
                return uri;
            }
            catch(Exception e)
            {
                return null;
            }
        }
    }
}
