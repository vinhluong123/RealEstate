using Microsoft.AspNetCore.Mvc;
using RealEstateServices.Models;

namespace RealEstateServices.Controllers
{
    [ApiController]
    [Route("[api/controller]")]
    public class ProductController : ControllerBase
    {
        [HttpGet(Name = "Get")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            try
            {
                //var students = await _context.Students.FindAsync(id);
                //return students;
                return null;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }
    }
}
