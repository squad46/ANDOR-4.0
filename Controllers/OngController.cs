using Andor.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;


namespace Andor.Controllers
{
    public class OngController : Controller
    {
        private readonly Contexto _context;

        public OngController(Contexto context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewData["ongs"] = _context.Ongs.ToList();
            return View();
        }
    }
}

