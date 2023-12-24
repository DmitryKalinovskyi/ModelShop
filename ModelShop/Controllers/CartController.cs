using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ModelShop.Data.Contracts;
using ModelShop.Models;
using ModelShop.Services;

namespace ModelShop.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;
        private readonly UserManager<Client> _userManager;

        public CartController(ICartService cartService,
            IOrderService orderService,
            UserManager<Client> userManager)
        {
            _cartService = cartService;
            _orderService = orderService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var cart = _cartService.GetCart(_userManager.GetUserId(User));

            return View(cart);
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int id) 
        {
            _cartService.RemoveFromCart(_userManager.GetUserId(User), id);

            return RedirectToAction("Index", "Cart");
        }

        [HttpPost]
        public IActionResult Buy()
        {
            try
            {
                _orderService.CompleteOrder(_userManager.GetUserId(User));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return RedirectToAction("Index", "Cart");
        }
    }
}
