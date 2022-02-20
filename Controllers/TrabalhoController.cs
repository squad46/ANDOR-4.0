using Andor.Models;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
using System.Linq;
//using System.Threading.Tasks;

namespace Andor.Controllers
{
    public class TrabalhoController : Controller
    {
        private readonly Contexto _context;

        public TrabalhoController(Contexto context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewData["trabalhos"] = _context.Trabalhos.OrderByDescending(x => x.DataCadastro).ToList(); // cria lista de trabalhos
            return View();
        }

        [HttpPost] // faz filtro de trabalhos por uf 
        public IActionResult Filtrar(string uf)
        {
            if (uf != "UF")
            {
                ViewData["trabalhos"] = _context.Trabalhos.OrderByDescending(x => x.DataCadastro).Where(p => p.UF == uf).ToList(); // cria lista de trabalhos com filtro por uf
            }
            else
            {
                ViewData["trabalhos"] = _context.Trabalhos.OrderByDescending(x => x.DataCadastro).ToList(); // cria lista de trabalhos sem filtro
            }
            return View("Index");
        }

        [HttpGet]
        public IActionResult Detalhes(int id, int pessoaId)
        {
            var trabalho = _context.Trabalhos.Where(p => p.Id == id).ToList();
            var pessoa   = _context.Pessoas.Where(p => p.Id == pessoaId).ToList();
            ViewData["_trabalhos"] = trabalho;
            ViewData["_pessoas"]   = pessoa;
            return View();
        }
    }
}
