using Andor.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Andor.Controllers
{
    public class ProfissionalController : Controller
    {
        private readonly Contexto _context;

        public ProfissionalController(Contexto context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var list = from f in _context.Formacoes
                       join p in _context.Pessoas on f.Id_pessoa equals p.Id
                       // where p.UF == "RJ" && f.Nome.Contains("Des")
                       select new Profissional
                       {
                           Id = f.Id,
                           Nome = f.Nome,
                           Descricao = f.Descricao,
                           Instituicao = f.Instituicao,
                           Inicio = f.Inicio,
                           Fim = f.Fim,
                           Situacao = f.Situacao,
                           PessoaNome = p.Nome,
                           PessoaUF = p.UF
                       };
            ViewData["profissional"] = list.ToList();

            /*
            ViewData["profissional"] = _context.Formacoes.Join(
                _context.Pessoas,
                formacao => formacao.Id_pessoa,
                pessoa => pessoa.Id,
                (formacao, pessoa) => new Profissional
                {
                    Id = formacao.Id,
                    Nome = formacao.Nome,
                    Descricao = formacao.Descricao,
                    Instituicao = formacao.Instituicao,
                    Inicio = formacao.Inicio,
                    Fim = formacao.Fim,
                    Situacao = formacao.Situacao,
                    PessoaNome = pessoa.Nome,
                    PessoaUF = pessoa.UF
                }
                
            ).ToList();
            */

            return View();
        }
    }
}

