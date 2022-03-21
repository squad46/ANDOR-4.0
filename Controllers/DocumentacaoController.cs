using Microsoft.AspNetCore.Mvc;

namespace Andor.Controllers
{
    public class DocumentacaoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
