using Microsoft.AspNetCore.Mvc;

namespace ModelShop.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
