using Andor.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

namespace Andor.Controllers
{
    public class ContatoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contato(string nome, string sobrenome, string telefone, string email, string sobre, string duvida)
        {
            if (nome == null || sobrenome == null || telefone == null || email == null || sobre == null || duvida == null) 
            {
                TempData["mensagem"] = "Todos os campos devem ser peenchidos.";
                return View("Index");
            }


            var destinatario = email;
            // envia para Andor 
            using (SmtpClient client = new SmtpClient("smtp-mail.outlook.com", 587)
            {
                Credentials = new NetworkCredential("andor.refugiados@outlook.com", "Recodepro"),
                EnableSsl = true
            })
            {
                var titulo = "ANDOR - Contato: "+ nome;
                var mensagem = "Olá, \n\n" +
                               "Mensagem de: " + nome + " " + sobrenome + ", Tel.: " + telefone + ", email: " + email +
                               "\nAssunto: " + sobre +
                               "\nMensagem: " + duvida;

                client.Send("andor.refugiados@outlook.com", "squad46recode@gmail.com", titulo, mensagem);
            }

            // envia confirmacao para pessoa
            using (SmtpClient client = new SmtpClient("smtp-mail.outlook.com", 587)
            {
                Credentials = new NetworkCredential("andor.refugiados@outlook.com", "Recodepro"),
                EnableSsl = true
            })
            {
                var titulo = "ANDOR - Contato";
                var mensagem = "Olá " + nome + ", \n\n" +
                               "Obrigado por entrar em contato! " +
                               "\nResponderemos sua mensagem em breve." +
                               "\n\nAtenciosamente equipe Andor";

                client.Send("andor.refugiados@outlook.com", destinatario, titulo, mensagem);
            }

            TempData["mensagem"] = "Recebemos a sua mensagem.";
            return View("Index");

        }



    }
}
