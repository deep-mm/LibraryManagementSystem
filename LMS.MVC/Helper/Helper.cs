using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace LMS.MVC.Helper
{
    public class Helper
    {
        public Helper(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public async Task<HttpClient> SetTokenAsync(HttpClient httpClient)
        {
            string resourceId = Configuration["Api:ResourceId"];
            string appId = Configuration["MVCClientId"];
            string appSecret = Configuration["MVCClientSecret"];
            string authority = $"{Configuration["AzureAd:Instance"]}{Configuration["MVCTenantId"]}";

            try
            {
                var authContext = new AuthenticationContext(authority);
                var credential = new ClientCredential(appId, appSecret);
                var authResult = await authContext.AcquireTokenAsync(resourceId, credential);
                var token = authResult.AccessToken;

                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                return httpClient;
            }
            catch(Exception e)
            {
                //Log Exception into Application Insights
                return null;
            }
        }
    }

    
}
