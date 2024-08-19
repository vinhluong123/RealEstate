using stackup_docker_db_demo.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace stackup_docker_db_demo.Repository
{
    public interface IBlogPostRepository
    {
        Task CreatePost(BlogPost post);
        Task<IEnumerable<BlogPost>> GetPost();
        Task<IEnumerable<BlogPost>> GetPost(string id);
        Task<IEnumerable<BlogPost>> UpdatePost(string id);
        Task<IEnumerable<BlogPost>> DeletePost(string id);
    }
}
