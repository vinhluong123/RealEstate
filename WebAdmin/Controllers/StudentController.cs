using Microsoft.AspNetCore.Mvc;

namespace WebAdmin.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
