using Andor.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Andor.Controllers
{
    public class ImagemController : Controller
    {
        private readonly Contexto _context;

        public ImagemController(Contexto context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<int> imagens = _context.Imagens.Select(m => m.Id).ToList();
            return View(imagens);
        }

        [HttpPost] // faz alteração o avatar
        public IActionResult UploadImagem(int id_pessoa, string tipo, IList<IFormFile> arquivos)
        {
            IFormFile imagemEnviada = arquivos.FirstOrDefault();
            try
            {
                imagemEnviada.ContentType.ToLower().StartsWith("image/");
                if (imagemEnviada.ContentType == "image/jpeg" || imagemEnviada.ContentType == "image/png")
                {

                    var imagemAntiga = _context.Imagens.Where(p => p.Id_tipo == id_pessoa).ToList(); // excluir imagem antiga do perfil 
                    foreach (var _imagemAntiga in imagemAntiga)
                    {
                        _context.Imagens.Remove(_imagemAntiga);
                        _context.SaveChanges();
                    }

                    MemoryStream ms = new MemoryStream(); // salva imagem de perfil
                    imagemEnviada.OpenReadStream().CopyTo(ms);
                    Imagem imagemEntity = new Imagem()
                    {
                        Id_tipo = id_pessoa,
                        Tipo = "perfil",
                        Nome = imagemEnviada.Name,
                        Dados = ms.ToArray(),
                        ContentType = imagemEnviada.ContentType
                    };
                    _context.Imagens.Add(imagemEntity);
                    _context.SaveChanges();
                }
            }
            catch
            { 
            }

            return Redirect("~/Pessoa/Edit/" + id_pessoa);
            //return RedirectToAction("Index");
        }

        public IActionResult VerImagem(int id, string tipo) // busca imagem
        {
            Imagem imagem = new Imagem();
            if (tipo == "moradia")
            {
                imagem = _context.Imagens.FirstOrDefault(m => m.Id == id && m.Tipo == tipo);
            }
            else if (tipo == "perfil")
            {
                imagem = _context.Imagens.FirstOrDefault(m => m.Id_tipo == id && m.Tipo == tipo);
            }
            else if (tipo == "moradia_lista") 
            {
                imagem = _context.Imagens.FirstOrDefault(m => m.Id_tipo == id && m.Tipo == "moradia" && m.Nome == "img1");
            }
            
            if (imagem != null)
            {
                MemoryStream ms = new MemoryStream(imagem.Dados);
                return new FileStreamResult(ms, imagem.ContentType);
            }
            return null;
        }

/*
        public IActionResult VerImagemMoradia(int id, string tipo) // busca imagem de moradia
        {
            Imagem imagem = _context.Imagens.FirstOrDefault(m => m.Id == id && m.Tipo == tipo);
            if (imagem != null)
            {
                MemoryStream ms = new MemoryStream(imagem.Dados);
                return new FileStreamResult(ms, imagem.ContentType);
            }
            return null;
        }
*/     

    }
}
