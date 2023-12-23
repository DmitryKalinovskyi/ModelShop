using Microsoft.AspNetCore.Mvc;
using ModelShop.Data.Contracts;
using ModelShop.Models;
using ModelShop.Services;
using ModelShop.ViewModels;

namespace ModelShop.Controllers
{
    public class Model3DController : Controller
    {
        private readonly IModel3DRepository _model3DRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IPhotoService _photoService;

        public Model3DController(IModel3DRepository model3DRepository,
            IClientRepository clientRepository, IPhotoService photoService)
        {
            _clientRepository = clientRepository;
            _model3DRepository = model3DRepository;
            _photoService = photoService;
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
        public async Task<IActionResult> Create(Model3DCreateViewModel model3DVM)
        {
            if(ModelState.IsValid == false)
            {
                return View(model3DVM);
            }

            var result = await _photoService.AddPhotoAsync(model3DVM.Image);

            if(result.Error != null)
            {
                ModelState.AddModelError("Image", $"Failed to post image: {result.Error.Message}");
                return View(model3DVM);
            }

            
            try
            {

                var model = new Model3D
                {
                    Title = model3DVM.Title,
                    Price = model3DVM.Price,
                    Description = model3DVM.Description,
                    ImageSource = result.Url.ToString(),
                    CreatedDate = DateTime.Now,
                    OwnerID = _clientRepository.GetAll().ToList()[0].Id,
                    ModelCategoryID = 2
                };

                _model3DRepository.Insert(model);
                _model3DRepository.Save();

                return Redirect($"Details/{model.Model3DID}");
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);

                ModelState.AddModelError("Title", $"Internal server error");
                // prevent creating image

                _photoService.DeletePhotoAsync(result.Url.ToString());
            }

            return View(model3DVM);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var model = _model3DRepository.Get(id);

            if (model == null)
                NotFound();

            await _photoService.DeletePhotoAsync(model.ImageSource);
            _model3DRepository.Delete(model);
            _model3DRepository.Save();

            return RedirectToAction("Index", "Home");
        }
    }
}
