using Microsoft.AspNetCore.Mvc;
using ModelShop.Data;
using ModelShop.Data.Contracts;
using ModelShop.Models;
using ModelShop.ViewModels;
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

        public async Task<IActionResult> Index()
        {
            var viewModel = new IndexViewModel
            { 
                Models3D = await _model3DRepository.GetAllAsync(),
                ModelCategories = await _modelCategoryRepository.GetAllAsync(),
                IsFindResult = false
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(IndexViewModel indexViewModel)
        {
            indexViewModel.Models3D = await _model3DRepository
                .SearchAsync(indexViewModel.Search, indexViewModel.MinPrice, indexViewModel.MaxPrice);

            indexViewModel.Models3D = indexViewModel.OrderBy switch
            {
                OrderBy.Date => indexViewModel.Models3D.OrderBy(m => m.CreatedDate),
                OrderBy.DateDescending => indexViewModel.Models3D.OrderByDescending(m => m.CreatedDate),
                OrderBy.Views => indexViewModel.Models3D.OrderBy(m => m.Views),
                OrderBy.ViewsDescending => indexViewModel.Models3D.OrderByDescending(m => m.Views),
                OrderBy.Price => indexViewModel.Models3D.OrderBy(m => m.Price),
                OrderBy.PriceDescending => indexViewModel.Models3D.OrderByDescending(m => m.Price)
            };


            indexViewModel.ModelCategories = await _modelCategoryRepository.GetAllAsync();
            indexViewModel.IsFindResult = true;
            //indexViewModel.MinPrice = 
            return View(indexViewModel);
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