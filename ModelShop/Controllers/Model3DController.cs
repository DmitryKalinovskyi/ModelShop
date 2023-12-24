using Microsoft.AspNetCore.Identity;
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
        private readonly IPhotoService _photoService;
        private readonly IFileService _fileService;
        private readonly UserManager<Client> _userManager;

        public Model3DController(IModel3DRepository model3DRepository, IPhotoService photoService,
            IFileService fileService, UserManager<Client> userManager)
        {
            _model3DRepository = model3DRepository;
            _photoService = photoService;
            _fileService = fileService;
            _userManager = userManager;
        }

        public IActionResult Details(int id)
        {
            var model = _model3DRepository.Get(id);
            var viewModel = new Model3DDetailViewModel
            {
                Model3D = model,
                IsOwner = model.OwnerID == _userManager.GetUserId(User),
            };

            model.Views++;
            _model3DRepository.Save();

            return View(viewModel);
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

            List<Action> toPrevent = new List<Action>();

            var prevent = () =>
            {
                foreach (var action in toPrevent)
                {
                    action();
                }
            };

            var result = await _photoService.AddPhotoAsync(model3DVM.Image);
            var result2 = await _fileService.AddFileAsync(model3DVM.Model3DFile);

            if(result.Error != null)
            {
                ModelState.AddModelError("Image", $"Failed to post image: {result.Error.Message}");
                return View(model3DVM);
            }

            toPrevent.Add(() => _photoService.DeletePhotoAsync(result.Url.ToString()));

            if (result2.Error != null)
            {
                ModelState.AddModelError("Image", $"Failed to post model: {result.Error.Message}");
                return View(model3DVM);
            }

            toPrevent.Add(() => _fileService.DeleteFileAsync(result2.Url.ToString()));

            try
            {
                var model = new Model3D
                {
                    Title = model3DVM.Title,
                    Price = model3DVM.Price,
                    Description = model3DVM.Description,
                    ImageSource = result.Url.ToString(),
                    FileSource = result2.Url.ToString(),
                    CreatedDate = DateTime.Now,
                    OwnerID = _userManager.GetUserId(User),
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
                prevent();
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
