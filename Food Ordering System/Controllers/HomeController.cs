using Microsoft.AspNetCore.Mvc;

namespace Food_Ordering_System.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
