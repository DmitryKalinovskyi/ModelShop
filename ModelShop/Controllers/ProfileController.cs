using Microsoft.AspNetCore.Mvc;
using ModelShop.Data;
using ModelShop.Data.Contracts;

namespace ModelShop.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IClientRepository _clientRepository;
        public ProfileController(IClientRepository clientRepository) 
        {
            _clientRepository = clientRepository;
        }


        public IActionResult Details(string id)
        {
            return View(_clientRepository.GetById(id));
        }
    }
}
