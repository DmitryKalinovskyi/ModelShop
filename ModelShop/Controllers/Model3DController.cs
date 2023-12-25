using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Abstractions;
using ModelShop.Data.Contracts;
using ModelShop.Models;
using ModelShop.Services;
using ModelShop.ViewModels;

namespace ModelShop.Controllers
{
    [Authorize]
    public class Model3DController : Controller
    {
        private readonly IModel3DRepository _model3DRepository;
        private readonly IModelCategoryRepository _modelCategoryRepository;
        private readonly IPhotoService _photoService;
        private readonly IFileService _fileService;
        private readonly ICartService _orderService;

        private readonly UserManager<Client> _userManager;

        public Model3DController(IModel3DRepository model3DRepository, IPhotoService photoService,
            ICartService orderService,
            IModelCategoryRepository modelCategoryRepository,
            IFileService fileService, UserManager<Client> userManager)
        {
            _model3DRepository = model3DRepository;
            _photoService = photoService;
            _fileService = fileService;
            _orderService = orderService;
            _modelCategoryRepository = modelCategoryRepository;
            _userManager = userManager;
        }

        public IActionResult Details(int id)
        {
            var model = _model3DRepository.Get(id);
            if (model == null)
            {
                ModelState.AddModelError("Error", "Model not founded!");
                return BadRequest(ModelState);
            }

            var userID = _userManager.GetUserId(User);

            var viewModel = new Model3DDetailViewModel
            {
                Model3D = model,
                IsOwner = model.OwnerID == userID,
                IsInCart = _orderService.IsInCart(userID, id),
                IsOwned = _orderService.IsOrdered(userID, id),
            };

            model.Views++;
            _model3DRepository.Save();

            return View(viewModel);
        }
        
        public IActionResult Create()
        {
            var viewModel = new Model3DCreateViewModel
            {
                ModelCategories = _modelCategoryRepository.GetAll()
            };

            return View(viewModel);
        }

        public IActionResult Edit(int? id)
        {
            if (id.HasValue == false) return BadRequest();

            var model = _model3DRepository.Get((int)id);
            if (model == null) return BadRequest();

            var viewModel = new Model3DEditViewModel
            {
                Title = model.Title,
                Description = model.Description,
                Price = model.Price,
                ModelCategoryID = model.ModelCategoryID,
                ModelCategories = _modelCategoryRepository.GetAll()
            };


            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Model3DEditViewModel viewModel, int? id)
        {
            if (id.HasValue == false) return BadRequest();

            var model = _model3DRepository.Get((int)id);
            if (model == null) return BadRequest();

            if(ModelState.IsValid == false)
            {
                viewModel.ModelCategories = _modelCategoryRepository.GetAll();
                return View(viewModel);
            }

			//var viewModel = new Model3DEditViewModel
			model.Title = viewModel.Title;
            model.Description = viewModel.Description;
            model.Price = viewModel.Price;
            model.ModelCategoryID = viewModel.ModelCategoryID;

            if (viewModel.Image != null)
            {
                try
                {
			        var result = await _photoService.AddPhotoAsync(viewModel.Image);
                    if(result.Error == null)
                    {
                        model.ImageSource = result.Url.ToString();
                    }
                }
                catch
                {
                }
            }

            if(viewModel.File != null)
            {
                try
                {
			        var result2 = await _fileService.AddFileAsync(viewModel.File);
                    if(result2.Error == null)
                    {
                        model.FileSource = result2.Url.ToString();
                    }
                }catch{ }
            }

			_model3DRepository.Save();

            return RedirectToAction("Details", "Model3D", new {id});
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int id)
        {
            try
            {
                _orderService.AddToCart(_userManager.GetUserId(User), id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return RedirectToAction("Details", "Model3D", new { id });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            try
            {
                _orderService.RemoveFromCart(_userManager.GetUserId(User), id);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return RedirectToAction("Details", "Model3D", new { id });
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(Model3DCreateViewModel model3DVM)
        {
            model3DVM.ModelCategories = await _modelCategoryRepository.GetAllAsync();

            //ModelState.Values
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
                    ImageSource = result.Url?.ToString(),
                    FileSource = result2.Url?.ToString(),
                    CreatedDate = DateTime.Now,
                    OwnerID = _userManager.GetUserId(User),
                    ModelCategoryID = model3DVM.ModelCategoryID
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
                return BadRequest("Model not found!");

            // check is have access or not
            //var client =  .Get(_userManager.GetUserId(User));



            await _photoService.DeletePhotoAsync(model.ImageSource);
            _model3DRepository.Delete(model);
            _model3DRepository.Save();

            return RedirectToAction("Index", "Home");
        }
    }
}
