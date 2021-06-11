
using LMS.SharedFiles.DTOs;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS.DataAccessLayer.Repositories
{
    public class AzureSearchService : IAzureSearchService
    {
        private SearchServiceClient searchServiceClient;
        private ISearchIndexClient searchIndexClient;

        public AzureSearchService(IConfiguration configuration)
        {
            searchServiceClient = new SearchServiceClient("lmssearchservice", new SearchCredentials(configuration["AzureSearchPrimaryKey"]));
            searchIndexClient = new SearchIndexClient("lmssearchservice", "cosmosdb-index", new SearchCredentials(configuration["AzureSearchPrimaryKey"]));
        }

        public async Task<IEnumerable<PostDTO>> Search(string searchValue)
        {
            SearchParameters parameters = new SearchParameters()
            {

            };

            DocumentSearchResult<PostDTO> documentSearchResult = await searchIndexClient.Documents.SearchAsync<PostDTO>(searchValue);

            List<PostDTO> postDTOs = new List<PostDTO>();
            foreach (SearchResult<PostDTO> result in documentSearchResult.Results)
            {
                postDTOs.Add(result.Document);
            }

            return postDTOs;
        }


    }
}
