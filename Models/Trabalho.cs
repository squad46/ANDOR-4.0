using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Andor.Models
{
    public class Trabalho
    {
        [Key]
        [Column(TypeName = "int")]
        public int Id { get; set; }
        [Column(TypeName = "int")]
        public int Id_pessoa { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string Instituicao { get; set; }
        [Column(TypeName = "nvarchar(40)")]
        public string Nome { get; set; }
        [Column(TypeName = "nvarchar(200)")]
        public string Atividade { get; set; }
        [Column(TypeName = "nvarchar(10)")]
        public string Tipo { get; set; }
        [Column(TypeName = "money")]
        public double Salario { get; set; }
        [Column(TypeName = "nvarchar(60)")]
        public string Endereco { get; set; }
        [Column(TypeName = "int")]
        public int Numero { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string Bairro { get; set; }
        [Column(TypeName = "int")]
        public int CEP { get; set; }
        [Column(TypeName = "nvarchar(2)")]
        public string UF { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string Cidade { get; set; }
        [Column(TypeName = "nvarchar(40)")]
        public string NomeContato { get; set; }
        [Column(TypeName = "nvarchar(13)")]
        public string TelefoneContato { get; set; }
        [Column(TypeName = "nvarchar(40)")]
        public string EmailContato { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataCadastro { get; set; }
        public Pessoa Pessoa { get; set; }
    }
}