using Microsoft.AspNetCore.Mvc;

namespace WebAdmin.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
