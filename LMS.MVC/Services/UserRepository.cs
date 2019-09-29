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
                    //Log Exception
                    tokenSet = false;
                }
            }
        }

        public async Task<bool> AddNewUser(UserDTO user)
        {
            try
            {
                await setToken();
                var myContent = JsonConvert.SerializeObject(user);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await httpClient.PostAsync($"api/users/addUser", byteContent);

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

        public async Task<IEnumerable<BookOrdersDTO>> GetBookHistory(int userId)
        {
            try
            {
                await setToken();
                var response = await httpClient.GetAsync($"api/users/history/{userId}");

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"{response.StatusCode}");
                }

                var stringData = response.Content.ReadAsStringAsync().Result;
                IEnumerable<BookOrdersDTO> data = JsonConvert.DeserializeObject<IEnumerable<BookOrdersDTO>>(stringData);
                return data;
            }
            catch (Exception e)
            {
                //Log Exception
                return null;
            }
        }

        public async Task<UserDTO> GetUserByName(string name)
        {
            try
            {
                await setToken();
                string url = "api/users/name/" + $"{Base64UrlEncoder.Encode(name)}";
                var response = await httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var stringData = response.Content.ReadAsStringAsync().Result;
                UserDTO data = JsonConvert.DeserializeObject<UserDTO>(stringData);
                return data;
            }
            catch (Exception e)
            {
                //Log Exception
                return null;
            }
        }
    }
}
