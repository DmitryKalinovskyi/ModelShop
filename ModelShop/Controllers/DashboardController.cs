using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ModelShop.Data.Contracts;
using ModelShop.Models;
using ModelShop.Services;
using ModelShop.ViewModels;
using System.Runtime.CompilerServices;

namespace ModelShop.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IClientRepository _clientRepository;
        private readonly UserManager<Client> _userManager;

        public DashboardController(IOrderService orderService,
            IClientRepository clientRepository,
            UserManager<Client> userManager)
        {
            _orderService = orderService;
            _clientRepository = clientRepository;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var client = _clientRepository.GetById(_userManager.GetUserId(User));
            var viewModel = new DashboardViewModel
            {
                OrderedModels = _orderService.GetOrderedModels3D(client.Id),
                OwnedModels = client.OwnedModels3D
            };


            return View(viewModel);
        }
    }
}
