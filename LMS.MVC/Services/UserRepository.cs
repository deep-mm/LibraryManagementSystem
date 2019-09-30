using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using LMS.SharedFiles.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace LMS.MVC.Services
{
    public class UserRepository : IUserRepository
    {
        private HttpClient httpClient;
        private readonly Helper.Helper helper;
        private bool tokenSet = false;
        private HttpResponseMessage response;
        private string className = "UserRepository";

        public UserRepository(HttpClient httpClient, IConfiguration configuration)
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
                    tokenSet = false;
                    throw new Exception(className + "/setToken(): Error occured while setting token", e);
                }
            }
        }

        public async Task<bool> AddNewUser(UserDTO user)
        {
            if (user != null)
            {
                await setToken();
                var myContent = JsonConvert.SerializeObject(user);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await httpClient.PostAsync($"api/users/addUser", byteContent);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(className + $"/AddNewUser(): {response.StatusCode}");
                }
                return true;
            }
            else
            {
                throw new ArgumentNullException(className + "/AddNewUser(): user object parameter in null");
            }
        }

        public async Task<IEnumerable<BookOrdersDTO>> GetBookHistory(int userId)
        {
            if (userId != 0)
            {
                await setToken();
                var response = await httpClient.GetAsync($"api/users/history/{userId}");

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(className + $"/GetBookHistory(): {response.StatusCode}");
                }

                var stringData = await response.Content.ReadAsStringAsync();
                IEnumerable<BookOrdersDTO> data = JsonConvert.DeserializeObject<IEnumerable<BookOrdersDTO>>(stringData);
                return data;
            }
            else
            {
                throw new ArgumentNullException(className + "/GetBookHistory(): userId object parameter in null");
            }
        }

        public async Task<UserDTO> GetUserByName(string name)
        {
            if (name != null)
            {
                await setToken();
                string url = "api/users/name/" + $"{Base64UrlEncoder.Encode(name)}";
                var response = await httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(className + $"/GetUserByName(): {response.StatusCode}");
                }

                var stringData = await response.Content.ReadAsStringAsync();
                UserDTO data = JsonConvert.DeserializeObject<UserDTO>(stringData);
                return data;
            }
            else
            {
                throw new ArgumentNullException(className + "/GetUserByName(): name string parameter in null");
            }
        }
    }
}
