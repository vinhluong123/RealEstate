using Microsoft.AspNetCore.Mvc;
using stackup_docker_db_demo.Model;
using stackup_docker_db_demo.Repository;
using System.Threading.Tasks;

namespace stackup_docker_db_demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogUserController : ControllerBase
    {
        private IBlogUserRepository _userRepository;

        public BlogUserController(IBlogUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetUsers()
        {
            var blogUsers = await _userRepository.GetBlogUser();
            return Ok(blogUsers);
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser( BlogUser blogUser)
        {
            await _userRepository.CreateUser(blogUser);
            return Ok(new JsonResult(blogUser));
        }
    }
}
