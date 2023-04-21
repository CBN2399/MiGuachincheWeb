using Microsoft.AspNetCore.Mvc;

namespace MiGuachincheWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
