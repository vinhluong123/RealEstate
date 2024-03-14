using stackup_docker_db_demo.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace stackup_docker_db_demo.Repository
{
    public interface IBlogUserRepository
    {
        Task CreateUser (BlogUser post);
        Task<IEnumerable<BlogUser>> GetBlogUser();
        Task<IEnumerable<BlogUser>> UpdateBlogUser(string id);
        Task<IEnumerable<BlogUser>> DeleteBlogUser(string id);
    }
}
