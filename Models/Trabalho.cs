using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Andor.Models
{
    public class Trabalho
    {
        [Key] [Column(TypeName = "int")]
        public int Id { get; set; }
        [Required] [Column(TypeName = "int")]
        public int Id_pessoa { get; set; }
        [Required] [Column(TypeName = "nvarchar(20)")][MinLength(2)][MaxLength(20)]
        public string Instituicao { get; set; }
        [Required] [Column(TypeName = "nvarchar(40)")][MinLength(3)][MaxLength(40)]
        public string Nome { get; set; }
        [Required] [Column(TypeName = "nvarchar(200)")][MinLength(3)][MaxLength(200)]
        public string Atividade { get; set; }
        [Required][Column(TypeName = "nvarchar(10)")][MaxLength(10)]
        public string Tipo { get; set; }
        [Required][Column(TypeName = "money")]
        public double Salario { get; set; }
        [Required][Column(TypeName = "nvarchar(60)")][MinLength(3)][MaxLength(60)]
        public string Endereco { get; set; }
        [Required][Column(TypeName = "int")]
        public int Numero { get; set; }
        [Required][Column(TypeName = "nvarchar(20)")][MinLength(2)][MaxLength(20)]
        public string Bairro { get; set; }
        [Required][Column(TypeName = "int")]
        public int CEP { get; set; }
        [Required][Column(TypeName = "nvarchar(2)")][MinLength(2)] [MaxLength(2)]
        public string UF { get; set; }
        [Required][Column(TypeName = "nvarchar(20)")][MinLength(3)][MaxLength(20)]
        public string Cidade { get; set; }
        [Required][Column(TypeName = "nvarchar(40)")][MinLength(3)][MaxLength(40)]
        public string NomeContato { get; set; }
        [Required][Column(TypeName = "nvarchar(13)")][MinLength(10)][MaxLength(13)]
        public string TelefoneContato { get; set; }
        [Required][Column(TypeName = "nvarchar(40)")][MinLength(4)][MaxLength(40)]
        public string EmailContato { get; set; }
        [Required][Column(TypeName = "datetime")]
        public DateTime DataCadastro { get; set; }
        public Pessoa Pessoa { get; set; }

        /*
        // Validação
        public string Validacao(Trabalho trabalho) 
        {
            string acumulador = "valido";

            if (trabalho.Instituicao == null || trabalho.Instituicao.Length > 20) { acumulador = "invalido"; }
            if (trabalho.Nome == null || trabalho.Nome.Length > 40) { acumulador = "invalido"; }
            if (trabalho.Atividade == null || trabalho.Atividade.Length > 200) { acumulador = "invalido"; }
            if (trabalho.Tipo == "-" || trabalho.Tipo == null || (trabalho.Tipo != "Autônomo" && trabalho.Tipo != "CLT" && trabalho.Tipo != "PJ" && trabalho.Tipo != "Temporário")) { acumulador = "invalido"; }
            if (trabalho.Endereco == null || trabalho.Endereco.Length > 60) { acumulador = "invalido"; }
            if (trabalho.Bairro == null || trabalho.Instituicao.Length > 20) { acumulador = "invalido"; }
            if (trabalho.UF == "-" || trabalho.UF == null || trabalho.UF.Length > 2) { acumulador = "invalido"; }
            if (trabalho.Cidade == null || trabalho.Cidade.Length > 20) { acumulador = "invalido"; }
            if (trabalho.NomeContato == null || trabalho.NomeContato.Length > 40) { acumulador = "invalido"; }
            if (trabalho.TelefoneContato == null || trabalho.TelefoneContato.Length > 13) { acumulador = "invalido"; }
            if (trabalho.EmailContato == null || trabalho.EmailContato.Length > 40) { acumulador = "invalido"; }
           
            return acumulador;
        }
        */
    }
}