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

        // verifica e manda email para recuperacao da senha
        public IActionResult PreparaRedefinicao(string destinatario) 
        {

            var verificaCad = _context.Pessoas.Where(p => p.Email == destinatario); // consulta email

            // envia email se a consulta retornar verdadeiro 
            if (verificaCad.Any())
            {
                // gera numero randomico entre 100000 e 999999
                Random randNum = new Random();
                var chave = randNum.Next(100000, 999999).ToString();

                using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("squad46recode@gmail.com", "recodepro"),
                    EnableSsl = true
                })
                {
                    var titulo = "ANDOR - Redefinição de senha.";
                    var mensagem = "Olá, \n\n" +
                                   "Sua chave para redefinição de senha é: " + chave +
                                   "\n\nAtenciosamente, " +
                                   "\nEquipe ANDOR";

                    client.Send("squad46recode@gmail.com", destinatario, titulo, mensagem);
                }
                HttpContext.Session.SetString("destinatario", destinatario); // cria sessao com o email do usuario
                HttpContext.Session.SetString("chave", chave); // cria sessao com o a chave enviada por email
            }
            
            return View("FormRedefinirSenha");
        }

        
        public IActionResult RedefinirSenha(string vChave, string nSenha, string rSenha)
        {
            if(vChave==null || nSenha == null || rSenha == null) 
            {
                ViewData["mensagem"] = "Todos os campos devem ser preenchidos";
                return View("FormRedefinirSenha");
            }
            var sessionChave = HttpContext.Session.GetString("chave");
            var sessionDestinatario = HttpContext.Session.GetString("destinatario");

            void MataSessao()
            {
                HttpContext.Session.Remove("chave");
                //HttpContext.Session.Remove("destinatario");
            }
            
            if (nSenha.Length < 6)
            {
                MataSessao();
                ViewData["mensagem"] = "Por favor digite uma senha com no mínimo 6 caracteres.";
                return View("FormRedefinirSenha");
            }
            
            nSenha = GetHash(nSenha); // hash sha-1
            rSenha = GetHash(rSenha); // hash sha-1

            var pessoa = _context.Pessoas.Where(p => p.Email == sessionDestinatario).FirstOrDefault();

            if (nSenha == rSenha)
            {
                if (pessoa != null && pessoa.Email == sessionDestinatario)
                {
                    pessoa.Senha = nSenha;
                    _context.Update(pessoa);
                    _context.SaveChanges();
                    ViewData["mensagem"] = "Senha atualizada com sucesso!";
                    MataSessao();
                }
                else
                {
                    MataSessao(); 
                    ViewData["mensagem"] = "Ops!, algo deu errado";
                    return View("FormRedefinirSenha");
                }
            }
            else
            {
                MataSessao();
                ViewData["mensagem"] = "Repita nova senha!";
                return View("FormRedefinirSenha");
            }
 
            return View("Index");

            /*
             Horas e horas tentando fazer esse código e a porcaria da session retornava sempre null.
             kkkkkk a porcaria da session estava configurada pra expirar em 10 segundos por padrão.
             A sessioon expirava antes que o email chegasse. 
             Agora a session dura 5 minutos >:) - Ferretti 
             */
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
