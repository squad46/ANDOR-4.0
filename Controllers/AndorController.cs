using Andor.Models;
using Microsoft.AspNetCore.Mvc;

namespace Andor.Controllers
{
    public class AndorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
