using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using LMS.MVC.Helper;
using LMS.SharedFiles.DTOs;
using Microsoft.AspNetCore.Http;
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
        private string className = "BookRepository";
        private ApplicationInsightsTracking applicationInsightsTracking;
        private IHttpContextAccessor httpContextAccessor;

        public BookRepository(HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            this.httpClient = httpClient;
            this.httpContextAccessor = httpContextAccessor;
            this.helper = new Helper.Helper(configuration);
            this.httpClient.BaseAddress = new Uri(configuration["Api:BaseUrl"]);
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            this.httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            applicationInsightsTracking = new ApplicationInsightsTracking();
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
                    tokenSet = false;
                    throw new Exception(className + "/setToken(): Error occured while setting token", e);
                }
            }
        }

        public async Task<bool> AddBook(BookDTO newBook, int libraryId)
        {
            if (newBook == null)
            {
                throw new ArgumentNullException(className + "/AddBook(): newBook object parameter in null");
            }
            else if (libraryId == 0)
            {
                throw new ArgumentNullException(className + "/AddBook(): libraryId object parameter in null");
            }
            else
            {
                await setToken();
                var myContent = JsonConvert.SerializeObject(newBook);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await httpClient.PostAsync($"api/books/addBook/{libraryId}", byteContent);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(className + $"/AddBook(): {response.StatusCode}");
                }
                return true;
            }
        }

        public async Task<bool> DeleteBook(int bookId)
        {
            if (bookId != 0)
            {
                await setToken();
                var response = await httpClient.DeleteAsync($"api/books/delete/{bookId}");

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(className + $"/DeleteBook(): {response.StatusCode}");
                }
                return true;
            }
            else
            {
                throw new ArgumentNullException(className + "/DeleteBook(): bookId object parameter in null");
            }
        
        }

        public async Task<bool> EditBook(BookDTO updatedBook)
        {
            if (updatedBook != null)
            {
                await setToken();
                var myContent = JsonConvert.SerializeObject(updatedBook);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await httpClient.PutAsync($"api/Books/updateBook", byteContent);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(className + $"/EditBook(): {response.StatusCode}");
                }
                return true;
            }
            else
            {
                throw new ArgumentNullException(className + "/EditBook(): updatedBook object parameter received as null");
            }
        }

        public async Task<BookDTO> GetBookById(int id)
        {
            if (id != 0)
            {
                await setToken();
                response = await httpClient.GetAsync($"api/books/id/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(className + $"/GetBookById(): {response.StatusCode}");
                }

                var stringData = await response.Content.ReadAsStringAsync();
                try
                {
                    BookDTO data = JsonConvert.DeserializeObject<BookDTO>(stringData);
                    return data;
                }
                catch(JsonSerializationException exception)
                {
                    throw new JsonSerializationException(className + "/GetBookById(): Error occured in Json Deserialization" , exception);
                }
            }
            else
            {
                throw new ArgumentNullException(className + "/GetBookById(): id object parameter received as null");
            }
        }

        public async Task<IEnumerable<BookDTO>> GetBooks(String searchTerm)
        {
            if (searchTerm != null)
            {
                await setToken();
                var libraryId = httpContextAccessor.HttpContext.Session.GetInt32("libraryId");
                if (libraryId != null)
                {
                    if (searchTerm.Equals(""))
                        response = await httpClient.GetAsync($"api/books/{libraryId.ToString()}");
                    else
                        response = await httpClient.GetAsync($"api/books/name/{searchTerm}/{libraryId.ToString()}");
                }
                else
                {
                    if (searchTerm.Equals(""))
                        response = await httpClient.GetAsync($"api/books/{"2"}");
                    else
                        response = await httpClient.GetAsync($"api/books/name/{searchTerm}/{"2"}");
                }
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(className + $"/GetBooks(): {response.StatusCode}");
                }

                var stringData = await response.Content.ReadAsStringAsync();
                try
                {
                    IEnumerable<BookDTO> data = JsonConvert.DeserializeObject<IEnumerable<BookDTO>>(stringData);
                    return data;
                }
                catch (JsonSerializationException exception)
                {
                    throw new JsonSerializationException(className + "/GetBooks(): Error occured in Json Deserialization", exception);
                }
            }
            else
            {
                throw new ArgumentNullException(className + "/GetBooks(): searchTerm string parameter received as null");
            }
        }

        public async Task<string> UploadImage(BookImageDTO bookImageDTO)
        {
            if (bookImageDTO != null)
            {
                await setToken();
                var myContent = JsonConvert.SerializeObject(bookImageDTO);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await httpClient.PostAsync($"api/books/uploadBookImage", byteContent);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(className + $"/UploadImage(): {response.StatusCode}");
                }
                var stringData = await response.Content.ReadAsStringAsync();
                return stringData;
            }
            else
            {
                throw new ArgumentNullException(className + "/UploadImage(): bookImageDTO object parameter received as null");
            }
        }
    }
}
