using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using stackup_docker_db_demo.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace stackup_docker_db_demo.Repository
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private IMongoClient _mongoClient;
        private IMongoDatabase _mongoDatabase;
        private IMongoCollection<BlogPost> _blogPosts { get; }

        public BlogPostRepository(IConfiguration config, IMongoClient _client)
        {
            _mongoClient = _client;
            _mongoDatabase = _mongoClient.GetDatabase(config["DatabaseSettings:PostgressDB"]);
            _blogPosts = _mongoDatabase.GetCollection<BlogPost>(config["DatabaseSettings:PostgressDBCollection"]);
        }

        public async Task CreatePost(BlogPost post)
        {
            await _blogPosts.InsertOneAsync(post);
        }

        public async Task<IEnumerable<BlogPost>> GetPost()
        {
            return await _blogPosts.Find(x => true).ToListAsync();
        }

        public Task<IEnumerable<BlogPost>> UpdatePost(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<BlogPost>> DeletePost(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}
