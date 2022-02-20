using Andor.Models;
using Microsoft.AspNetCore.Mvc;

namespace Andor.Controllers
{
    public class FeedController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
