using CitiesManager.ClientWeb.Models;
using CitiesManger.ClientWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace CitiesManger.ClientWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            List<City> list = new List<City>();
            using(var client = new HttpClient()){
                using (var response = await client.GetAsync("https://localhost:7141/api/Cities1"))
                {
                    string api = await response.Content.ReadAsStringAsync();
                    list = JsonConvert.DeserializeObject<List<City>>(api);
                }
                    
            }
            return View(list);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}