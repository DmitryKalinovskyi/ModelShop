using Microsoft.AspNetCore.Mvc;
using ModelShop.Data;
using ModelShop.Models;
using System.Diagnostics;

namespace ModelShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ModelShopContext _context;


        public HomeController(ModelShopContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ModelList()
        {
            var models = _context.Models3D.ToList();

            return View(models);
        }

        public IActionResult Category()
        {
            var categories = _context.ModelCategories.ToList();

            return View(categories);
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