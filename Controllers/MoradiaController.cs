using Andor.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Andor.Controllers
{
    public class MoradiaController : Controller
    {
        private readonly Contexto _context;

        public MoradiaController(Contexto context)
        {
            _context = context;
        }

        public IActionResult Index(int? id)
        {
            ViewData["moradias"] = _context.Moradias.OrderByDescending(x => x.DataCadastro).ToList();     // cria lista de moradias 
            //return View("Detalhes");
            return View();
        }

        [HttpPost] // faz filtro de moradias por uf 
        public IActionResult Filtrar(string uf)
        {
            if (uf != "UF")
            {
                ViewData["moradias"] = _context.Moradias.OrderByDescending(x => x.DataCadastro).Where(p => p.UF == uf).ToList(); // cria lista de moradias com filtro por uf
            }
            else
            {
                ViewData["moradias"] = _context.Moradias.OrderByDescending(x => x.DataCadastro).ToList(); // cria lista de moradias sem filtro
            }
            return View("Index");
        }


        [HttpGet] // detalhes de moradia
        public IActionResult Detalhes(int id, int pessoaId)
        {
            var moradia = _context.Moradias.Where(p => p.Id == id).ToList();
            var pessoa  = _context.Pessoas.Where(p => p.Id == pessoaId).ToList();
            var imagens = _context.Imagens.Where(p => p.Id_tipo == id && p.Tipo == "moradia").ToList();

            ViewData["imagens"] = imagens;
            ViewData["_moradias"] = moradia;
            ViewData["_pessoas"] = pessoa;
            return View();
        }

        public IActionResult Novo(int? id)
        {
            if (Request.Cookies["Id"] == null) // 404 caso usuario não esteja logado
            {
                return NotFound();
            }
            
            ViewData["Id_pessoa"] = Request.Cookies["Id"];
            return View();
        }

        [HttpPost] // Adiciona anuncio de moradia
        public IActionResult Novo([Bind("Id,Id_pessoa,Name,Descricao,Tipo,Preco,Endereco,Bairro,Numero,CEP,UF,Cidade,NomeContato,TelefoneContato,EmailContato,DataCadastro")] Moradia moradia,
            IList<IFormFile> img1, IList<IFormFile> img2, IList<IFormFile> img3, IList<IFormFile> img4)
        {
      
            var idPessoa = moradia.Id_pessoa;
            IFormFile imagemEnviada = img1.FirstOrDefault();
            if (imagemEnviada != null)
            {
                _context.Add(moradia);
                _context.SaveChanges();
            }
            else
            {
                var msg = "Imagem de capa deve ser escolhida";
                ViewData["Id_pessoa"] = Request.Cookies["id"];
                ViewBag.mensagem = msg;
                return View();
            }
            SalvaImagens(img1, moradia.Id);
            SalvaImagens(img2, moradia.Id);
            SalvaImagens(img3, moradia.Id);
            SalvaImagens(img4, moradia.Id);

            return Redirect("~/Pessoa/Details/" + Request.Cookies["id"]);
        }

        public void SalvaImagens(IList<IFormFile> img, int moradia) // Salva imagens
        {
            IFormFile imagemEnviada = img.FirstOrDefault();
            if (imagemEnviada != null)
            {
                imagemEnviada.ContentType.ToLower().StartsWith("image/");
                if (imagemEnviada.ContentType == "image/jpeg" || imagemEnviada.ContentType == "image/png")
                {
                    MemoryStream ms = new MemoryStream(); // salva imagem de moradia
                    imagemEnviada.OpenReadStream().CopyTo(ms);
                    Imagem imagemEntity = new Imagem()
                    {
                        Id_tipo = moradia,
                        Tipo = "moradia",
                        Nome = imagemEnviada.Name,
                        Dados = ms.ToArray(),
                        ContentType = imagemEnviada.ContentType
                    };
                    _context.Imagens.Add(imagemEntity);
                    _context.SaveChanges();
                }
            }
        }

        [HttpPost]// Exclui moradia e retorna para o perfil
        public IActionResult Excluir(Moradia moradia)
        {
            if (Request.Cookies["Id"] == null) // 404 caso usuario não esteja logado
            {
                return NotFound();
            }

            var moradiaDelete = _context.Moradias.Find(moradia.Id);
            var imagemMoradia = _context.Imagens.Where(p => p.Id_tipo == moradia.Id && p.Tipo == "moradia").ToList();
            var idPessoa = moradiaDelete.Id_pessoa;
            _context.Remove(moradiaDelete);
            _context.SaveChanges();

            foreach (var imagem in imagemMoradia) // exclui imagens de moradia
            {
                _context.Imagens.Remove(imagem);
                _context.SaveChanges();
            }

            return Redirect("~/Pessoa/Details/" + idPessoa);
        }
    }
}
