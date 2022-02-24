using Andor.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace Andor.Controllers
{
    public class LoginController : Controller
    {
        private readonly Contexto _context;

        public LoginController(Contexto context) 
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        // converte a senha com hash sha-1
        public static string GetHash(string input)
        {
            return string.Join("", (new SHA1Managed().ComputeHash(Encoding.UTF8.GetBytes(input))).Select(x => x.ToString("X2")).ToArray()).ToString();
        }

        [HttpPost]
        public IActionResult Logar(string email, string senha)
        {
            senha = GetHash(senha); //hash sha-1
            //ViewData["email"] = email;
            //ViewData["password"] = senha;
            var verificaLogin = _context.Pessoas.Where(p => p.Email == email && p.Senha == senha).ToList();

            if (verificaLogin.Count == 1)
            {
                foreach (var pessoa in verificaLogin)
                {
                    var idPessoa = pessoa.Id;
                    Response.Cookies.Append("Id", idPessoa.ToString());         // Cria cookie com id
                    Response.Cookies.Append("Usuario", pessoa.Nome.ToString()); // Cria cookie com nome
                    ViewData["id"] = idPessoa;
                    ViewData["mensagem"] = "Bem vindo " + pessoa.Nome + ", login efetuado com sucesso!";
                }
            }
            else
            {
                ViewData["mensagem"] = "Email ou senha inválidos!";
                return View("Index");
            }

            //return View();
            return Redirect("~/Home");
        }

        public IActionResult Logout()
        {
            //cookies excluidos para que seja feito logoff
            Response.Cookies.Delete("Id");
            Response.Cookies.Delete("Usuario");
            return Redirect("~/Home");
        }

        public IActionResult EditarSenha(int? id) 
        {
            if (id == null)
            {
                return NotFound();
            }

            if (id.ToString() != Request.Cookies["Id"].ToString())  // Compara se parametro id é diferente de id passado no cookie 
            {
                return NotFound();
            }

            var pessoa = _context.Pessoas.Find(id);
            if (pessoa == null)
            {
                return NotFound();
            }

            return View(pessoa);
        }

        // POST: Editar senha
        [HttpPost]
        public IActionResult EditarSenha(int id, string senha, string nSenha, string rSenha)
        {
            senha  = GetHash(senha);  // hash sha-1
            nSenha = GetHash(nSenha); // hash sha-1
            rSenha = GetHash(rSenha); // hash sha-1

            var pessoa = _context.Pessoas.Find(id);
            if (nSenha == rSenha)
            {
                //var pessoa = _context.Pessoas.Find(id);
                if (pessoa != null && pessoa.Senha == senha)
                {
                    pessoa.Senha = nSenha;
                    _context.Update(pessoa);
                    _context.SaveChanges();
                    ViewData["mensagem"] = "Senha atualizada com sucesso!";
                }
                else
                {
                    ViewData["mensagem"] = "Senha inválida!";
                }
            }
            else
            {
                ViewData["mensagem"] = "Repita nova senha!";
            }
            return View(pessoa);
        }

        // view recuperar senha
        public IActionResult RecuperarSenha() 
        {
            return View();
        }

        // manda email para recuperacao da senha
        public IActionResult RedefinirSenha()
        {

            using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("squad46recode@gmail.com", "recodepro"),
                EnableSsl = true
            })
            {
                var destinatario = "eliferretti14@gmail.com";
                var titulo       = "ANDOR - Redefineção de senha.";
                var mensagem     = "mensagem para redefinição de senha.";

                client.Send("squad46recode@gmail.com", destinatario, titulo, mensagem);
            }
            return View("Index");
        }


        //---------------  Refêrencias -----------------

        // https://www.youtube.com/watch?v=Y0X2nDJa3pc
        // https://www.youtube.com/watch?v=BduylSCRDhU
        //  Response.Cookies.Append("nome", "valor"); 

        /*
        public void Set(string key, string value, int? expireTime)
        {
            CookieOptions option = new CookieOptions();

            if (expireTime.HasValue)
                option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
            else
                option.Expires = DateTime.Now.AddMilliseconds(10);

            Response.Cookies.Append(key, value, option);
        }

      
        //read cookie from IHttpContextAccessor  
        string cookieValueFromContext = _httpContextAccessor.HttpContext.Request.Cookies["key"];

        //read cookie from Request object  
        string cookieValueFromReq = Request.Cookies["Key"];
        */

    }
}
