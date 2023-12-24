using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ModelShop.Data;
using ModelShop.Data.Contracts;
using ModelShop.Models;
using ModelShop.ViewModels;

namespace ModelShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly IClientRepository _clientRepository;
        private readonly UserManager<Client> _userManager;
        private readonly SignInManager<Client> _signInManager;

        public AccountController(UserManager<Client> userManager, SignInManager<Client> signInManager, IClientRepository clientRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _clientRepository = clientRepository;
        }

        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid == false) return View(loginViewModel);

            var client = await _userManager.FindByEmailAsync(loginViewModel.Email);

            if (client == null)
            {
                ModelState.AddModelError("Email", "No such user with that email!");
                return View(loginViewModel);
            }

            if (await _userManager.CheckPasswordAsync(client, loginViewModel.Password) == false)
            {
                ModelState.AddModelError("Password", "Passwords do not match!");
                return View(loginViewModel);
            }

            var result = await _signInManager.PasswordSignInAsync(client, loginViewModel.Password, false, false);

            if (result.Succeeded == false)
            {
                ModelState.AddModelError("Password", "Internal server error");
                return View(loginViewModel);
            }



            //var client = await _clientRepository.GetByIdAsync(loginViewModel.Email);

            //if (client == null)
            //{
            //    ModelState.AddModelError("Username", "User with this username don't exist!");
            //    return View(loginViewModel);
            //}

            //// check is valid login, if valid add to cookies as logined
            //var passwordHash = PasswordHasher.Hash(loginViewModel.Password);
            //if (passwordHash != client.PasswordHash)
            //{
            //    ModelState.AddModelError("Password", "Incorrect password!");
            //    return View(loginViewModel);
            //}

            //HttpContext.Session.SetString("username", loginViewModel.Username);
            //HttpContext.Session.SetString("passwoordHash", passwordHash);


            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if(ModelState.IsValid == false)
            {
                return View(registerViewModel);
            }

            var client = await _userManager.FindByNameAsync(registerViewModel.Username);

            if (client != null)
            {
                ModelState.AddModelError("Username", "Some already use this username!");
                return View(registerViewModel);
            }

            // check does exist user with this email
            client = await _userManager.FindByEmailAsync(registerViewModel.Email);

            if(client != null)
            {
                ModelState.AddModelError("Email", "Some already use this email!");
                return View(registerViewModel);
            }

            // create new client
            client = new Client
            {
                Firstname = registerViewModel.Firstname,
                Lastname = registerViewModel.Lastname,
                UserName = registerViewModel.Username,
                Email = registerViewModel.Email,
                RegisterDate = DateTime.Now,
                Cart = new Cart()
            };

            var newUserResponse = await _userManager.CreateAsync(client, registerViewModel.Password);

            if (newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(client, UserRoles.User);

                var result = await _signInManager.PasswordSignInAsync(client, registerViewModel.Password, false, false);

                if (result.Succeeded == false)
                {
                    ModelState.AddModelError("Password", "Internal server error");
                    return View(registerViewModel);
                }

                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach(var error in newUserResponse.Errors)
                {
                    ModelState.AddModelError("Password", error.Description);
                }
            }

            return View(registerViewModel);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
