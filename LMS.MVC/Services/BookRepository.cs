using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using LMS.SharedFiles.DTOs;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace LMS.MVC.Services
{
    public class BookRepository : IBookRepository
    {
        private HttpClient httpClient;
        private readonly Helper.Helper helper;
        private bool tokenSet = false;
        private HttpResponseMessage response;

        public BookRepository(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.helper = new Helper.Helper(configuration);
            this.httpClient.BaseAddress = new Uri(configuration["Api:BaseUrl"]);
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            this.httpClient.DefaultRequestHeaders.Accept.Add(contentType);
        }
        public async Task setToken()
        {
            if (!tokenSet)
            {
                try
                {
                    httpClient = await helper.SetTokenAsync(httpClient);
                    tokenSet = true;
                }
                catch(Exception e)
                {
                    //Log Exception
                    tokenSet = false;
                }
            }
        }

        public async Task<bool> AddBook(BookDTO newBook, int libraryId)
        {
            try
            {
                await setToken();
                var myContent = JsonConvert.SerializeObject(newBook);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await httpClient.PostAsync($"api/books/addBook/{libraryId}", byteContent);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"{response.StatusCode}");
                }
                return true;
            }
            catch(Exception e)
            {
                //Log Exception
                return false;
            }
        }

        public async Task<bool> DeleteBook(int bookId)
        {
            try
            {
                await setToken();
                var response = await httpClient.DeleteAsync($"api/books/delete/{bookId}");

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"{response.StatusCode}");
                }
                return true;
            }
            catch (Exception e)
            {
                //Log Exception
                return false;
            }
        }

        public async Task<bool> EditBook(BookDTO updatedBook)
        {
            try
            {
                await setToken();
                var myContent = JsonConvert.SerializeObject(updatedBook);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await httpClient.PutAsync($"api/Books/updateBook", byteContent);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"{response.StatusCode}");
                }
                return true;
            }
            catch (Exception e)
            {
                //Log Exception
                return false;
            }
        }

        public async Task<BookDTO> GetBookById(int id)
        {
            try
            {
                await setToken();
                response = await httpClient.GetAsync($"api/books/id/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"{response.StatusCode}");
                }

                var stringData = response.Content.ReadAsStringAsync().Result;
                BookDTO data = JsonConvert.DeserializeObject<BookDTO>(stringData);
                return data;
            }
            catch (Exception e)
            {
                //Log Exception
                return null;
            }
        }

        public async Task<IEnumerable<BookDTO>> GetBooks(String searchTerm)
        {
            try
            {
                await setToken();
                if (searchTerm.Equals(""))
                    response = await httpClient.GetAsync($"api/books");
                else
                    response = await httpClient.GetAsync($"api/books/name/{searchTerm}");

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"{response.StatusCode}");
                }

                var stringData = response.Content.ReadAsStringAsync().Result;
                IEnumerable<BookDTO> data = JsonConvert.DeserializeObject<IEnumerable<BookDTO>>(stringData);
                return data;
            }
            catch (Exception e)
            {
                //Log Exception
                return null;
            }
        }

        public async Task<string> UploadImage(BookImageDTO bookImageDTO)
        {
            try
            {
                await setToken();
                var myContent = JsonConvert.SerializeObject(bookImageDTO);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await httpClient.PostAsync($"api/books/uploadBookImage", byteContent);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"{response.StatusCode}");
                }
                var stringData = await response.Content.ReadAsStringAsync();
                return stringData;
            }
            catch (Exception e)
            {
                //Log Exception
                return null;
            }
        }
    }
}
