﻿using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using stackup_docker_db_demo.Model;
using stackup_docker_db_demo.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace stackup_docker_db_demo.DBProviders
{
    public class MongoProvider : IBlogPostRepository
    {
		public IMongoClient _mongoClient;
		public IMongoDatabase _mongoDatabase;
		public IMongoCollection<BlogPost> _blogPosts { get; }

        public MongoProvider(IConfiguration config, IMongoClient _client)
        {
            _mongoClient = _client;
            _mongoDatabase = _mongoClient.GetDatabase(config["DatabaseSettings:MongoDB"]);
            _blogPosts = _mongoDatabase.GetCollection<BlogPost>(config["DatabaseSettings:MongoCollection"]);
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

        public async Task<IEnumerable<BlogPost>> GetPost(string id)
        {
            return (IEnumerable<BlogPost>)await _blogPosts.Find(b => b.Id.Equals(id)).FirstOrDefaultAsync();
        }
    }
}