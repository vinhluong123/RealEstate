using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using stackup_docker_db_demo.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace stackup_docker_db_demo.Repository
{
    public class BlogUserRepository : IBlogUserRepository
    {
        private IMongoClient _mongoClient;
        private IMongoDatabase _mongoDatabase;
        private IMongoCollection<BlogUser> _blogUsers { get; }
        public BlogUserRepository(IConfiguration config, IMongoClient _client)
        {
            _mongoClient = _client;
            _mongoDatabase = _mongoClient.GetDatabase(config["DatabaseSettings:PostgressDB"]);
            _blogUsers = _mongoDatabase.GetCollection<BlogUser>("users");
        }
        public async Task CreateUser(BlogUser user)
        {
          await _blogUsers.InsertOneAsync(user);
        }

        public Task<IEnumerable<BlogUser>> DeleteBlogUser(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<BlogUser>> GetBlogUser()
        {
            return await _blogUsers.Find<BlogUser>(x => true).ToListAsync();
        }

        public Task<IEnumerable<BlogUser>> UpdateBlogUser(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}
