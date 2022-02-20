using System;

namespace Andor.Models
{
    public class VmTrabalho
    {
        public int Id { get; set; } 
        public int Id_pessoa { get; set; }
        public string NomeContato { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Nome { get; set; }
        public  string Atividade { get; set; }
        public int Id_imagem { get; set; }
    }
}
