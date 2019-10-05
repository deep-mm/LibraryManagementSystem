using System;
using System.Collections;
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
    public class LibraryRepository : ILibraryRepository
    {
        private HttpClient httpClient;
        private readonly Helper.Helper helper;
        private bool tokenSet = false;
        private HttpResponseMessage response;
        private IHttpContextAccessor httpContextAccessor;
        private string className = "LibraryRepository";

        public LibraryRepository(HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            this.httpClient = httpClient;
            this.httpContextAccessor = httpContextAccessor;
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
                catch (Exception e)
                {
                    tokenSet = false;
                    throw new Exception(className + "/setToken(): Error occured while setting token" , e);
                }
            }
        }
        public async Task<bool> CheckoutBook(int bookId, int userId)
        {
            if (bookId == 0)
            {
                throw new ArgumentNullException(className + "/CheckoutBook(): The bookId object parameter is null");
            }
            else if (userId == 0)
            {
                throw new ArgumentNullException(className + "/CheckoutBook(): The userId object parameter is null");
            }
            else
            {
                await setToken();
                var response = await httpClient.GetAsync($"api/library/checkout/{bookId}/{userId}");

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(className + $"/CheckoutBook(): {response.StatusCode}");
                }
                return true;
            }
        }

        public async Task<IEnumerable<BookDTO>> GetAvailaibleBooks(string bookName)
        {
            await setToken();
            if(bookName.Equals(""))
                response = await httpClient.GetAsync($"api/library/availaibleBooks");
            else
                response = await httpClient.GetAsync($"api/library/availaibleBooks/{bookName}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(className + $"/GetAvailaibleBooks(): {response.StatusCode}");
            }

            var stringData = await response.Content.ReadAsStringAsync();
            try
            {
                IEnumerable<BookDTO> data = JsonConvert.DeserializeObject<IEnumerable<BookDTO>>(stringData);
                return data;
            }
            catch (JsonSerializationException exception)
            {
                throw new JsonSerializationException(className + "/GetAvailaibleBooks(): Error occured in Json Deserialization", exception);
            }

        }

        public async Task<bool> ReturnBook(int bookId, int userId)
        {
            if (bookId == 0)
            {
                throw new ArgumentNullException(className + "/ReturnBook(): The bookId object parameter is null");
            }
            else if (userId == 0)
            {
                throw new ArgumentNullException(className + "/ReturnBook(): The userId object parameter is null");
            }

            await setToken();
            var response = await httpClient.GetAsync($"api/library/return/{bookId}/{userId}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(className + $"/ReturnBook(): {response.StatusCode}");
            }
            return true;
        }

        public async Task<IEnumerable<LocationDTO>> GetAllLocations()
        {
            await setToken();
            var response = await httpClient.GetAsync($"api/library/allLocations");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(className + $"/GetAllLocations(): {response.StatusCode}");
            }

            var stringData = await response.Content.ReadAsStringAsync();
            try
            {
                IEnumerable<LocationDTO> data = JsonConvert.DeserializeObject<IEnumerable<LocationDTO>>(stringData);
                return data;
            }
            catch (JsonSerializationException exception)
            {
                throw new JsonSerializationException(className + "/GetAllLocations(): Error occured in Json Deserialization", exception);
            }
        }

        public async Task<IEnumerable<LibraryDTO>> GetLibrariesByLocation(int locationId)
        {
            await setToken();
            var response = await httpClient.GetAsync($"api/library/getLibrariesByLocation/{locationId.ToString()}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(className + $"/GetLibrariesByLocation(): {response.StatusCode}");
            }

            var stringData = await response.Content.ReadAsStringAsync();
            try
            {
                IEnumerable<LibraryDTO> data = JsonConvert.DeserializeObject<IEnumerable<LibraryDTO>>(stringData);
                return data;
            }
            catch (JsonSerializationException exception)
            {
                throw new JsonSerializationException(className + "/GetLibrariesByLocation(): Error occured in Json Deserialization", exception);
            }
        }

        public async Task<IEnumerable<PostDTO>> GetPosts()
        {
            await setToken();

            string searchTerm = httpContextAccessor.HttpContext.Session.GetString("SearchPost");
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                var response = await httpClient.GetAsync($"api/library/getPosts");
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(className + $"/GetPosts(): {response.StatusCode}");
                }

                var stringData = await response.Content.ReadAsStringAsync();
                try
                {
                    IEnumerable<PostDTO> data = JsonConvert.DeserializeObject<IEnumerable<PostDTO>>(stringData);
                    return data;
                }
                catch (JsonSerializationException exception)
                {
                    throw new JsonSerializationException(className + "/GetPosts(): Error occured in Json Deserialization", exception);
                }
            }
            else
            {
                IEnumerable<PostDTO> posts = await SearchPosts(searchTerm);
                return posts;
            }
        }

        public async Task<bool> AddPost(PostDTO post)
        {
            if (post == null)
            {
                throw new ArgumentNullException(className + "/AddPost(): post object parameter in null");
            }
            else
            {
                await setToken();
                var response = await httpClient.PostAsJsonAsync($"api/library/addPost", post);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(className + $"/AddPost(): {response.StatusCode}");
                }
                return true;
            }
        }

        public async Task<IEnumerable<PostDTO>> SearchPosts(string searchTerm)
        {
            if (searchTerm != null)
            {
                await setToken();
                var response = await httpClient.GetAsync($"api/library/searchPosts/{searchTerm}");
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(className + $"/SearchPosts(): {response.StatusCode}");
                }

                var stringData = await response.Content.ReadAsStringAsync();
                try
                {
                    IEnumerable<PostDTO> data = JsonConvert.DeserializeObject<IEnumerable<PostDTO>>(stringData);
                    return data;
                }
                catch (JsonSerializationException exception)
                {
                    throw new JsonSerializationException(className + "/SearchPosts(): Error occured in Json Deserialization", exception);
                }
            }
            else
            {
                throw new ArgumentNullException(className + "/SearchPosts(): The searchterm parameter receieved is null");
            }
        }
    }
}
