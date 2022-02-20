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
        {/*
            var  _trabalhos = _context.Trabalhos.Join(
                    _context.Imagens,
                    trabalho => trabalho.Id_pessoa,
                    imagem => imagem.Id_tipo,
                    (trabalho, imagem) => new
                    {
                        Id = trabalho.Id,
                        Id_pessoa = trabalho.Id_pessoa,
                        NomeContato = trabalho.NomeContato,
                        DataCadastro = trabalho.DataCadastro,
                        Nome = trabalho.Nome,
                        Atividade = trabalho.Atividade,
                        //Id_imagem = imagem.Id,
                    }
                ).ToList();

            ViewData["_trabalhos"] = _trabalhos;
            */
            ViewData["trabalhos"] = _context.Trabalhos.OrderByDescending(x => x.DataCadastro).ToList();    // cria lista de trabalhos
            ViewData["moradias"]  = _context.Moradias.OrderByDescending(x => x.DataCadastro).ToList();     // cria lista de moradias 
            return View();
        }
    }
}
