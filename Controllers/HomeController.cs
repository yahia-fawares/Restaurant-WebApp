using Microsoft.AspNetCore.Mvc;
using MyRestaurant.Models;
using System.Diagnostics;

namespace MyRestaurant.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ModelContext _context;
        public HomeController(ILogger<HomeController> logger, ModelContext context)
        {
            _logger = logger;
            _context = context;
        }




        public IActionResult Index()
        {

            var categories = _context.UserCategories.ToList();


            return View(categories);
        }

        public IActionResult GetCategoryById(int id)
        {
            var product = _context.UserProducts.Where(x=>x.CategoryId == id).ToList();
            return View(product);
        }

       
        public IActionResult Menu()
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
