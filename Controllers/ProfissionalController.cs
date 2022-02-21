using Andor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Andor.Controllers
{
    public class ProfissionalController : Controller
    {
        private readonly Contexto _context;

        public ProfissionalController(Contexto context)
        {
            _context = context;
        }

        public async Task<IActionResult> View()
        {
            /*
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
            var profissional = await _context.Formacoes.Join(
                _context.Pessoas,
                formacao => formacao.Id_pessoa,
                pessoa => pessoa.Id,
                (formacao, pessoa) => new
                {
                    Id = formacao.Id,
                    Nome = formacao.Nome,
                    Descicao = formacao.Descricao,
                    Instituicao = formacao.Instituicao,
                    Inicio = formacao.Inicio,
                    Fim = formacao.Fim,
                    Situacao = formacao.Situacao,
                    PessoaNome = pessoa.Nome,
                    PessoaUF = pessoa.UF
                }
            ).ToListAsync();

          //  ViewData["formacoes"] = _context.Formacoes.OrderByDescending(x => x.Id).ToList(); // cria lista de formações
            return View(profissional);
        }
    }
}
