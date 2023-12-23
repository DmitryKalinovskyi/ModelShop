using Microsoft.AspNetCore.Mvc;
using ModelShop.Data;
using ModelShop.Data.Contracts;
using ModelShop.Models;
using System.Diagnostics;

namespace ModelShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IModel3DRepository _model3DRepository;
        private readonly IModelCategoryRepository _modelCategoryRepository;

        public HomeController(IModel3DRepository model3DRepository,
            IModelCategoryRepository modelCategoryRepository,
            ILogger<HomeController> logger)
        {
            _model3DRepository = model3DRepository;
            _modelCategoryRepository = modelCategoryRepository;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ModelList()
        {
            var models = _model3DRepository.GetAll();

            return View(models);
        }

        public IActionResult Category()
        {
            var categories = _modelCategoryRepository.GetAll();

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