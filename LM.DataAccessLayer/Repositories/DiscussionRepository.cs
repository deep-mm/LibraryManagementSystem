using AutoMapper;
using LMS.DataAccessLayer.Entities;
using LMS.SharedFiles.DTOs;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DataAccessLayer.Repositories
{
    public class DiscussionRepository : IDiscussionRepository
    {
        private readonly IMapper mapper;
        private readonly IDistributedCache distributedCache;
        private CosmosClient cosmosClient;
        private Database database;
        private Container container;
        private string className = "DiscussionRepository";

        public DiscussionRepository(IConfiguration configuration, IMapper mapper, IDistributedCache distributedCache)
        {
            cosmosClient = new CosmosClient(configuration["CosmosDBConnectionString"]);
            database = cosmosClient.GetDatabase("LMS");
            container = database.GetContainer("Discussion");
            this.mapper = mapper;
            this.distributedCache = distributedCache;
        }

        public async Task<bool> AddNewPost(PostDTO post)
        {
            if (post != null)
            {
                Post mappedPost = mapper.Map<Post>(post);
                await container.CreateItemAsync<Post>(mappedPost, new PartitionKey(mappedPost.type));
                await ClearCache("Post_GetAll");
                return true;
            }
            else
            {
                throw new ArgumentNullException(className + "/AddNewPost(): The post object parameter received is null");
            }
        }

        public async Task<IEnumerable<PostDTO>> GetAllPosts()
        {
            IEnumerable<PostDTO> postDTOs;
            var cachedData = await distributedCache.GetStringAsync("Post_GetAll");

            if (cachedData == null)
            {
                var sqlQuery = "SELECT * FROM x";
                QueryDefinition queryDefinition = new QueryDefinition(sqlQuery);
                FeedIterator<Post> feedIterator = container.GetItemQueryIterator<Post>(queryDefinition);

                List<Post> posts = new List<Post>();

                while (feedIterator.HasMoreResults)
                {
                    FeedResponse<Post> currentResult = await feedIterator.ReadNextAsync();
                    foreach (Post post in currentResult)
                    {
                        posts.Add(post);
                    }
                }
                postDTOs = mapper.Map<IEnumerable<PostDTO>>(posts);

                DistributedCacheEntryOptions options = new DistributedCacheEntryOptions();
                options.SetAbsoluteExpiration(new TimeSpan(24, 0, 0));
                
                await distributedCache.SetStringAsync("Post_GetAll", JsonConvert.SerializeObject(postDTOs));
            }
            else
            {
                postDTOs = JsonConvert.DeserializeObject<IEnumerable<PostDTO>>(cachedData);
            }
            
            return postDTOs;
        }

        public async Task ClearCache(string key)
        {
            await distributedCache.RemoveAsync(key);
        }
    }
}
