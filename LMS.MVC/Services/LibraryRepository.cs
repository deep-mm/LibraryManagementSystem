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
    public class LibraryRepository : ILibraryRepository
    {
        private HttpClient httpClient;
        private readonly Helper.Helper helper;
        private bool tokenSet = false;
        private HttpResponseMessage response;

        public LibraryRepository(HttpClient httpClient, IConfiguration configuration)
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
                catch (Exception e)
                {
                    //Log Exception
                    tokenSet = false;
                }
            }
        }
        public async Task<bool> CheckoutBook(int bookId, int userId)
        {
            try
            {
                await setToken();
                var response = await httpClient.GetAsync($"api/library/checkout/{bookId}/{userId}");

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

        public async Task<IEnumerable<BookDTO>> GetAvailaibleBooks()
        {
            try
            {
                await setToken();
                response = await httpClient.GetAsync("api/library/availaibleBooks");

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

        public async Task<bool> ReturnBook(int bookId, int userId)
        {
            try
            {
                await setToken();
                var response = await httpClient.GetAsync($"api/library/return/{bookId}/{userId}");

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
    }
}
