using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ModelShop.Data.Contracts;
using ModelShop.Models;

namespace ModelShop.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository;
        private readonly UserManager<Client> _userManager;

        public CartController(ICartRepository cartRepository, UserManager<Client> userManager)
        {
            _cartRepository = cartRepository;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            // retrive client with cart
            var cart = _cartRepository.GetCartByClientId(_userManager.GetUserId(User));

            return View(cart);
        }
    }
}
