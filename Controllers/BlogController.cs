using Microsoft.AspNetCore.Mvc;

namespace Andor.Controllers
{
    public class BlogController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
