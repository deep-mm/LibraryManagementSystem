
using Microsoft.Azure.Search;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.DataAccessLayer.Repositories
{
    public class AzureSearchService
    {
        private SearchServiceClient searchServiceClient;
        public AzureSearchService(IConfiguration configuration)
        {
            searchServiceClient = new SearchServiceClient("lms-tone", new SearchCredentials(configuration["AzureSearchPrimaryKey"]));
        }


    }
}
