using Microsoft.AspNetCore.Mvc;
using ModelShop.Data.Contracts;
using ModelShop.ViewModels;

namespace ModelShop.Controllers
{
    public class AdminController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        private const int PAGE_SIZE = 16;

        public AdminController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> IndexAsync(AdminViewModel adminViewModel)
        {
            if (User.IsInRole("admin") == false) return BadRequest("You don't have acess to this information");

            var orders = await _orderRepository.GetAllAsync();

            if(adminViewModel.Page == null)
                adminViewModel.Page = 1;
            adminViewModel.PageCount = (int)Math.Ceiling((double)orders.Count / PAGE_SIZE);
            adminViewModel.Orders = _orderRepository.GetAll().AsQueryable()
                .Skip(PAGE_SIZE * ((int)adminViewModel.Page - 1))
                .Take(PAGE_SIZE).ToList();

            return View(adminViewModel);
        }
    }
}
