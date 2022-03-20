using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Andor.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Collections;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Andor.Controllers
{
    public class PessoaController : Controller
    {
        private readonly Contexto _context;

        public PessoaController(Contexto context)
        {
            _context = context;
        }

        // GET: Pessoa
        public async Task<IActionResult> Index()
        {
            return NotFound();
            //return View(await _context.Pessoas.ToListAsync());
        }

        // GET: Pessoa/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (id.ToString() != Request.Cookies["Id"].ToString())  // Compara se parametro id é diferente de id passado no cookie 
            {
                return NotFound();
            }

            var pessoa = await _context.Pessoas.FirstOrDefaultAsync(m => m.Id == id);

            ViewData["temAvatar"] = _context.Imagens.Where(m => m.Id_tipo == id && m.Tipo == "perfil").Count(); // verifica se tem avatar
            ViewData["formacoes"] = _context.Formacoes.Where(p => p.Id_pessoa == id).ToList();    // cria lista de formacoes da pessoa
            ViewData["experiencias"] = _context.Experiencias.Where(p => p.Id_pessoa == id).ToList(); // cria lista de experiencias da pessoa
            ViewData["trabalhos"] = _context.Trabalhos.Where(p => p.Id_pessoa == id).ToList();    // cria lista de trabalhos publicados pela pessoa
            ViewData["moradias"] = _context.Moradias.Where(p => p.Id_pessoa == id).ToList();     // cria lista de Moradias publicados pela pessoa

            if (pessoa == null)
            {
                return NotFound();
            }

            return View(pessoa);
        }

        // GET: Pessoa/Create
        public IActionResult Create()
        {
            return View();
        }

        // converte a senha com hash sha-1
        public static string GetHash(string input)
        {
            return string.Join("", (new SHA1Managed().ComputeHash(Encoding.UTF8.GetBytes(input))).Select(x => x.ToString("X2")).ToArray()).ToString();
        }

        //  Pessoa/Create - cadastro novo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Email,Senha,DataCadastro")] Pessoa pessoa, IList<IFormFile> arquivos)
        {
            //verifica se nome tem minimo de 3 digitos
            if (pessoa.Nome == null || pessoa.Nome.Length < 3)
            {
                ViewData["mensagem"] = "Nome deve ter no mínimo 3 dígitos.";
                return View("../Pessoa/Create");
            }

            // verifica se email é null
            if (pessoa.Email == null)
            {
                ViewData["mensagem"] = "Por favor, informe seu email";
                return View("../Pessoa/Create");
            }
            else if (new EmailAddressAttribute().IsValid(pessoa.Email) == false) // verifica se email é válido
            {
                ViewData["mensagem"] = "Por favor, informe um email válido.";
                return View("../Pessoa/Create");
            }
            else
            {
                //verifica se email já foi cadastrado e retorna mensagem se sim
                var verificaEmail = _context.Pessoas.Where(p => p.Email == pessoa.Email).ToList();
                if (verificaEmail.Count > 0)
                {
                    ViewData["mensagem"] = "Este email já foi cadastrado!";
                    return View("../Login/Index");
                }
            }

            // verifica se senha possui minimo de 6 digitos
            if (pessoa.Senha == null || pessoa.Senha.Length < 6)
            {
                ViewData["mensagem"] = "Senha deve ter no mínimo 6 dígitos e ao menos 1 letra minúscula, 1 letra maiúscula, 1 número e pode conter caracteres especiais.";
                return View("../Pessoa/Create");
            }

            IFormFile imagemEnviada = arquivos.FirstOrDefault();
            if (imagemEnviada != null && ModelState.IsValid) // verifica se os campos de email, senha e imagem foram preenchidos
            {
                /*
                pessoa.Senha = GetHash(pessoa.Senha);

                _context.Add(pessoa);
                await _context.SaveChangesAsync();
                */
                imagemEnviada.ContentType.ToLower().StartsWith("image/");
                if (imagemEnviada.ContentType == "image/jpeg" || imagemEnviada.ContentType == "image/png") // confirma se o formato da imagem é png ou jpg para continuar
                {
                    // hash senha
                    pessoa.Senha = GetHash(pessoa.Senha);

                    // salva cad pessoa
                    _context.Add(pessoa);
                    await _context.SaveChangesAsync();

                    // salva imagem do perfil
                    MemoryStream ms = new MemoryStream();
                    imagemEnviada.OpenReadStream().CopyTo(ms);
                    Imagem imagemEntity = new Imagem()
                    {
                        Id_tipo = pessoa.Id,
                        Tipo = "perfil",
                        Nome = imagemEnviada.Name,
                        Dados = ms.ToArray(),
                        ContentType = imagemEnviada.ContentType
                    };
                    _context.Imagens.Add(imagemEntity);
                    _context.SaveChanges();
                }
                else
                {
                    ViewData["mensagem"] = "Selecione uma imagem válida.";
                    return View("../Pessoa/Create");
                }

                ViewData["mensagem"] = "Usuário cadastrado com sucesso!";
                return View("../Login/Index");
            }
            else
            {
                ViewData["mensagem"] = "Todos os campos devem ser preenchidos.";
                return View("../Pessoa/Create");
            }
        }

        // GET: Pessoa/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            if (id.ToString() != Request.Cookies["Id"].ToString())  // Compara se parametro id é diferente de id passado no cookie 
            {
                return NotFound();
            }

            var pessoa = await _context.Pessoas.FindAsync(id);
            if (pessoa == null)
            {
                return NotFound();
            }
            ViewData["temAvatar"] = _context.Imagens.Where(m => m.Id_tipo == id).Count();
            return View(pessoa);
            //return Redirect("~/Pessoa/Details/" + id);
        }

        // POST: Pessoa/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Email,Senha,Telefone,CRNM,CPF,Endereco,Bairro,UF,Cidade,Numero,CEP,Sexo,DataNascimento,Nacionalidade,DataCadastro")] Pessoa pessoa)
        {

            if (id != pessoa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pessoa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PessoaExists(pessoa.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return Redirect("~/Pessoa/Details/" + id);

            }

            return Redirect("~/Pessoa/Details/" + id);

        }

        // GET: Pessoa/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (id.ToString() != Request.Cookies["Id"].ToString())  // Compara se parametro id é diferente de id passado no cookie 
            {
                return NotFound();
            }

            ViewData["temAvatar"] = _context.Imagens.Where(m => m.Id_tipo == id).Count();
            var pessoa = await _context.Pessoas.FirstOrDefaultAsync(m => m.Id == id);

            if (pessoa == null)
            {
                return NotFound();
            }

            return View(pessoa);
        }

        // POST: Pessoa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // deleta moradias anunciadas
            var moradia = _context.Moradias.Where(p => p.Id_pessoa == id).ToList();
            foreach (var _moradia in moradia)
            {
                var imagemMoradia = _context.Imagens.Where(p => p.Id_tipo == _moradia.Id && p.Tipo == "moradia").ToList();
                foreach (var imagem in imagemMoradia)
                {
                    _context.Imagens.Remove(imagem);
                    _context.SaveChanges();
                }
                _context.Moradias.Remove(_moradia);
                await _context.SaveChangesAsync();
            }

            // deleta trabalhos anunciados
            var trabalho = _context.Trabalhos.Where(p => p.Id_pessoa == id).ToList();
            foreach (var _trabalho in trabalho)
            {
                _context.Trabalhos.Remove(_trabalho);
                await _context.SaveChangesAsync();
            }

            // deleta avatar
            var imagemAvatar = _context.Imagens.Where(p => p.Id_tipo == id && p.Tipo == "perfil").ToList();
            foreach (var _imagemAvatar in imagemAvatar)
            {
                _context.Imagens.Remove(_imagemAvatar);
                await _context.SaveChangesAsync();
            }

            // deleta o perfil
            var pessoa = await _context.Pessoas.FindAsync(id);
            _context.Pessoas.Remove(pessoa);
            await _context.SaveChangesAsync();

            //cookies excluidos para que seja feito logoff
            Response.Cookies.Delete("Id");
            Response.Cookies.Delete("Usuario");

            return Redirect("~/Home");

        }


        [HttpGet] // exibicao do perfil publico
        public IActionResult PerfilPublico(int id)
        {
            if (Request.Cookies["Id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var pessoa = _context.Pessoas.Where(p => p.Id == id).ToList();
            if (pessoa != null)
            {
                ViewData["temAvatar"] = _context.Imagens.Where(m => m.Id_tipo == id && m.Tipo == "perfil").Count();
                ViewData["perfil"] = pessoa;
                return View(pessoa);
            }
            return NotFound();

        }

        [HttpGet] // exibicao do perfil profissional  
        public IActionResult PerfilProfissional(int id)
        {
            if (Request.Cookies["Id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var pessoa = _context.Pessoas.Where(p => p.Id == id).ToList();
            if (pessoa != null)
            {
                ViewData["temAvatar"] = _context.Imagens.Where(m => m.Id_tipo == id && m.Tipo == "perfil").Count();
                ViewData["perfil"] = pessoa;
                ViewData["formacao"] = _context.Formacoes.Where(p => p.Id_pessoa == id).ToList(); // faz lista de formacao profissional 
                ViewData["experiencia"] = _context.Experiencias.Where(p => p.Id_pessoa == id).ToList(); // faz lista com experiencia profissional 
                return View(pessoa);
            }
            return NotFound();

        }

        //---------------------------- inicio controles de formacao --------------------------------------------------------------------
        // GET: Formacao/Create
        public IActionResult formacaoCreate(int? Id_pessoa)
        {

            Pessoa pessoa = _context.Pessoas.Find(Id_pessoa);
            var verificaCampos = pessoa.VerificaCadastroCompleto(pessoa);
            if (verificaCampos != null)
            {
                TempData["verificaCampos"] = verificaCampos;
                return RedirectToRoute(new { controller = "Pessoa", action = "Details", id = Id_pessoa });
            }
            ViewData["Id_pessoa"] = Id_pessoa;
            return View();
        }

        // POST: Formacao/Create - Adiciona formacao profissional no perfil
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> formacaoCreate([Bind("Id,Id_pessoa,Nome,Descricao,Instituicao,Inicio,Fim,Situacao")] Formacao formacao)
        {
            if (ModelState.IsValid)
            {
                var idPessoa = formacao.Id_pessoa;
                _context.Add(formacao);
                await _context.SaveChangesAsync();
                return Redirect("~/Pessoa/Details/" + idPessoa);
            }
            else 
            {
                TempData["mensagem"] = "Ops! Algo deu errado.";
                return RedirectToRoute(new { controller = "Pessoa", action = "formacaoCreate", Id_pessoa = formacao.Id_pessoa });
            }
           // return Redirect("~/Pessoa");
        }


        [HttpPost] // Exclui formacao e retorna ao perfil
        public IActionResult deletarFormacao(Formacao formacao)
        {
            var formacaoDelete = _context.Formacoes.Find(formacao.Id);
            var idPessoa = formacaoDelete.Id_pessoa;
            _context.Remove(formacaoDelete);
            _context.SaveChanges();
            return Redirect("~/Pessoa/Details/" + idPessoa);
        }
        //------------------------ fim controles de formacao --------------------------------------------------------------------------


        //------------------------------- inicio controles de experiencia -------------------------------------------------------------

        // GET: Experiencia/Create
        public IActionResult experienciaCreate(int? Id_pessoa)
        {
            Pessoa pessoa = _context.Pessoas.Find(Id_pessoa);
            var verificaCampos = pessoa.VerificaCadastroCompleto(pessoa);
            if (verificaCampos != null)
            {
                TempData["verificaCampos"] = verificaCampos;
                return RedirectToRoute(new { controller = "Pessoa", action = "Details", id = Id_pessoa });
            }
            ViewData["Id_pessoa"] = Id_pessoa;
            return View();
        }

        // Adiciona experiencia profissional no perfil 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> experienciaCreate([Bind("Id,Id_pessoa,Nome,Descricao,Cargo,Instituicao,Inicio,Fim")] Experiencia experiencia)
        {
            if (ModelState.IsValid)
            {
                var idPessoa = experiencia.Id_pessoa;
                _context.Add(experiencia);
                await _context.SaveChangesAsync();
                return Redirect("~/Pessoa/Details/" + idPessoa);
            }
            else
            {
                TempData["mensagem"] = "Ops! Algo deu errado.";
                return RedirectToRoute(new { controller = "Pessoa", action = "formacaoCreate", Id_pessoa = experiencia.Id_pessoa });
            }
            //return Redirect("~/Pessoa");
        }


        [HttpPost]// Exclui experiencia e retorna para o perfil
        public IActionResult deletarExperiencia(Experiencia experiencia)
        {
            var experienciaDelete = _context.Experiencias.Find(experiencia.Id);
            var idPessoa = experienciaDelete.Id_pessoa;
            _context.Remove(experienciaDelete);
            _context.SaveChanges();
            return Redirect("~/Pessoa/Details/" + idPessoa);
        }
        // -------------------------- fim controles de experiencia ---------------------------------------------------------


        //---------------------------- inicio controles de Trabalho --------------------------------------------------------------------
        // GET: Trabalho/Create
        public IActionResult trabalhoCreate(int? Id_pessoa)
        {
            Pessoa pessoa = _context.Pessoas.Find(Id_pessoa);
            var verificaCampos = pessoa.VerificaCadastroCompleto(pessoa);
            if (verificaCampos != null)
            {
                TempData["verificaCampos"] = verificaCampos;
                return RedirectToRoute(new { controller = "Pessoa", action = "Details", id = Id_pessoa });
            }
            ViewData["Id_pessoa"] = Id_pessoa;
            return View();
        }

        // POST: Trabalho/Create - Adiciona trabalho no perfil
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> trabalhoCreate([Bind("Id,Id_pessoa,Instituicao,Nome,Atividade,Tipo,Salario,Endereco,Numero,Bairro,CEP,UF,Cidade,NomeContato,TelefoneContato,EmailContato,DataCadastro")] Trabalho trabalho)
        {
            if (ModelState.IsValid)
            {
                var idPessoa = trabalho.Id_pessoa;
                _context.Add(trabalho);
                await _context.SaveChangesAsync();
                return Redirect("~/Pessoa/Details/" + idPessoa);
            }
            else 
            {
                TempData["mensagem"] = "Ops! Algo deu errado.";
                return RedirectToRoute(new { controller = "Pessoa", action = "trabalhoCreate", Id_pessoa = trabalho.Id_pessoa });
            }
        }


        [HttpPost] // Exclui oferta de trabalho e retorna ao perfil
        public IActionResult deletarTrabalho(Trabalho trabalho)
        {
            var trabalhoDelete = _context.Trabalhos.Find(trabalho.Id);
            var idPessoa = trabalhoDelete.Id_pessoa;
            _context.Remove(trabalhoDelete);
            _context.SaveChanges();
            return Redirect("~/Pessoa/Details/" + idPessoa);
        }
        //------------------------ fim controles de trabalho --------------------------------------------------------------------------

        private bool PessoaExists(int id)
        {
            return _context.Pessoas.Any(e => e.Id == id);
        }
    }
}
