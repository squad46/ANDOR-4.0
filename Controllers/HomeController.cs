using Andor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Andor.Controllers
{
    public class HomeController : Controller
    {

        private readonly Contexto _context;

        public HomeController(Contexto context)
        {
            _context = context; 
        }

        public IActionResult Index()
        {
            //ViewData["id"] = Request.Cookies["Id"];
            ViewData["trabalhos"] = _context.Trabalhos.OrderByDescending(x => x.DataCadastro).ToList();    // cria lista de trabalhos
            ViewData["moradias"]  = _context.Moradias.OrderByDescending(x => x.DataCadastro).ToList();     // cria lista de moradias 
            return View();
        }

        public IActionResult EmpregosServicos()
        {
            return View();
        }
    }
}
