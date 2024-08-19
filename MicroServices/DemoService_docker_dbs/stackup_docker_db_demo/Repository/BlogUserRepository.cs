using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
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
            _mongoDatabase = _mongoClient.GetDatabase(config["DatabaseSettings:MongoDB"]);
            _blogUsers = _mongoDatabase.GetCollection<BlogUser>("users");
        }
        public async Task CreateUser(BlogUser user)
        {
            var userJson = "{\r\n... _id : ObjectId(\"507f191e810c19729de860ea\"),\r\n... title: \"MongoDB Overview\",\r\n... description: \"MongoDB is no sql database\",\r\n... by: \"tutorials point\",\r\n... url: \"http://www.tutorialspoint.com\",\r\n... tags: ['mongodb', 'database', 'NoSQL'],\r\n... likes: 100\r\n... })\r\nWriteResult({ \"nInserted\" : 1 }";
            var test = user.ToBsonDocument();
            await _blogUsers.InsertOneAsync(userJson);
        }   

        public Task<IEnumerable<BlogUser>> DeleteBlogUser(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<BlogUser>> GetBlogUser()
        {
            var users = await _blogUsers.Find<BlogUser>(x => true).ToListAsync();
            return users;
        }

        public Task<IEnumerable<BlogUser>> UpdateBlogUser(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}
