using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ModelShop.Data;
using ModelShop.Data.Contracts;
using ModelShop.Models;
using ModelShop.Services;
using ModelShop.ViewModels;

namespace ModelShop.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IClientRepository _clientRepository;
        private readonly IFollowingService _followingService;

        private readonly UserManager<Client> _userManager;
        public ProfileController(IClientRepository clientRepository,
            IFollowingService followingService, UserManager<Client> userManager) 
        {
            _clientRepository = clientRepository;
            _userManager = userManager;
            _followingService = followingService;
        }


        public IActionResult Details(string? id)
        {
            var userId = _userManager.GetUserId(User);
            var viewModel = new ProfileViewModel
            {
                Client = _clientRepository.GetById(id),
                IsOwner = id == userId,
                Views = _clientRepository.GetViewsById(id),
                Followed = _followingService.IsFollower(id, userId),
                Followers = _followingService.GetFollowers(id).Count,
                Followings = _followingService.GetFollowings(id).Count
            };


            return View(viewModel);
        }

        public IActionResult Current()
        {
            string? id = _userManager.GetUserId(User);

            return RedirectToAction("Details", "Profile", new { id });
        }

        [HttpPost]
        public IActionResult Follow(string id)
        {
            try
            {
                _followingService.Follow(id, _userManager.GetUserId(User));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return RedirectToAction("Details", "Profile", new {id});
        }

        [HttpPost]
        public IActionResult Unfollow(string id)
        {
            try
            {
                _followingService.Unfollow(id, _userManager.GetUserId(User));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return RedirectToAction("Details", "Profile", new { id });
        }


        public IActionResult Edit()
        {
            var client = _clientRepository.GetById(_userManager.GetUserId(User));
            return View(new ProfileEditViewModel
            {
                Firstname = client.Firstname,
                Lastname = client.Lastname,
                Description = client.Description
            });
        }

        [HttpPost]
        public IActionResult Edit(ProfileEditViewModel profileEditViewModel)
        {
            if(ModelState.IsValid == false) 
            {
                return View(profileEditViewModel);
            }

            // update profile
            var client = _clientRepository.GetById(_userManager.GetUserId(User));

            client.Firstname = profileEditViewModel.Firstname;
            client.Lastname = profileEditViewModel.Lastname;
            client.Description = profileEditViewModel.Description;

            _clientRepository.Save();

            return RedirectToAction("Current", "Profile");
        }
    }
}
