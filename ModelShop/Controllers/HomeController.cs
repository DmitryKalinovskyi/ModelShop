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

        private const int PAGE_SIZE = 8;

        public HomeController(IModel3DRepository model3DRepository,
            IModelCategoryRepository modelCategoryRepository,
            ILogger<HomeController> logger)
        {
            _model3DRepository = model3DRepository;
            _modelCategoryRepository = modelCategoryRepository;
            _logger = logger;
        }

        //public async Task<IActionResult> Index(IndexViewModel indexViewModel, int? id)
        //{
        //    //if (indexViewModel != null) return View(indexViewModel);

        //    //var models = (await _model3DRepository.GetAllAsync()).AsQueryable();
        //    //indexViewModel.ModelCategories = await _modelCategoryRepository.GetAllAsync();
        //    //indexViewModel.IsFindResult = false;
        //    //indexViewModel.Page = id ?? 1;
        //    //indexViewModel.PageCount = (int)Math.Ceiling((double)models.Count() / PAGE_SIZE);
        //    //indexViewModel.Models3D = models.Skip(((id ?? 1) - 1) * PAGE_SIZE).Take(PAGE_SIZE).ToList();

        //    return View(indexViewModel);
        //}

        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> IndexAsync(IndexViewModel indexViewModel)
        {
            indexViewModel.Models3D = await _model3DRepository
                .SearchAsync(indexViewModel.Search, indexViewModel.MinPrice, indexViewModel.MaxPrice);

            //if (id.HasValue) indexViewModel.Page = (int)id;
            if (Request.Method == "POST")
            {
                indexViewModel.IsFindResult = true;
                indexViewModel.ResultsCount = indexViewModel.Models3D.Count();
            }

            var models = indexViewModel.OrderBy switch
            {
                OrderBy.Date => indexViewModel.Models3D.AsQueryable().OrderBy(m => m.CreatedDate),
                OrderBy.DateDescending => indexViewModel.Models3D.AsQueryable().OrderByDescending(m => m.CreatedDate),
                OrderBy.Views => indexViewModel.Models3D.AsQueryable().OrderBy(m => m.Views),
                OrderBy.ViewsDescending => indexViewModel.Models3D.AsQueryable().OrderByDescending(m => m.Views),
                OrderBy.Price => indexViewModel.Models3D.AsQueryable().OrderBy(m => m.Price),
                OrderBy.PriceDescending => indexViewModel.Models3D.AsQueryable().OrderByDescending(m => m.Price),
                _ => indexViewModel.Models3D.AsQueryable().OrderBy(m => m.CreatedDate)
            };

            // divide into pages
            indexViewModel.PageCount = (int)Math.Ceiling((double)models.Count() / PAGE_SIZE);

            //this line causes timeout
            indexViewModel.Models3D = models.Skip((indexViewModel.Page - 1) * PAGE_SIZE)
                .Take(PAGE_SIZE).ToList();

            //if (id != null) indexViewModel.Page = (int)id;


            indexViewModel.ModelCategories = await _modelCategoryRepository.GetAllAsync();
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