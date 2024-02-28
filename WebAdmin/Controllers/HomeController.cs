using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebAdmin.Models;
using WebAdmin.Services.Interfaces;

namespace WebAdmin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWeatherForecastService _forecastService;

        public HomeController(ILogger<HomeController> logger, IWeatherForecastService forecastService)
        {
            _logger = logger;
            _forecastService = forecastService ?? throw new ArgumentNullException(nameof(forecastService));
        }

        /// <summary>
        /// Merged (Dev & Staging)That are comment define by Staging branch, that will conflick with Develop, that should resolving when merge code
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()            
        {
            try
            {
                var weatherforecast = await _forecastService.Find();
                return View(weatherforecast);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString(), ex);
            }
            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Add note for error too, from Staging branch
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}