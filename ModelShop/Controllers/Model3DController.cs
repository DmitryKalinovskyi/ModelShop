using Microsoft.AspNetCore.Mvc;
using ModelShop.Data.Contracts;
using ModelShop.Models;

namespace ModelShop.Controllers
{
    public class Model3DController : Controller
    {
        private readonly IModel3DRepository _model3DRepository;
        public Model3DController(IModel3DRepository model3DRepository)
        {
            _model3DRepository = model3DRepository;
        }

        public IActionResult Details(int id)
        {
            return View(_model3DRepository.Get(id));
        }

        
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Model3D model3D)
        {
            if(ModelState.IsValid == false)
            {
                //// Log or handle validation errors
                //foreach (var key in ModelState.Keys)
                //{
                //    var errors = ModelState[key].Errors;
                //    foreach (var error in errors)
                //    {
                //        // Log or handle individual validation errors
                //        var errorMessage = error.ErrorMessage;
                //        var exception = error.Exception; // You can check for exception details
                //                                         // Perform any additional logging or handling based on the error
                //    }
                //}

                return View(model3D);
            }

            model3D.CreatedDate = DateTime.Now;
            model3D.OwnerID = 1;
            model3D.ModelCategoryID = 2;

            _model3DRepository.Insert(model3D);
            _model3DRepository.Save();

            return Redirect($"Details/{model3D.Model3DID}");
        }
    }
}
