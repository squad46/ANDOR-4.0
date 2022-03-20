using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Andor.Models
{
    public class Moradia
    {
        [Key]
        [Column(TypeName = "int")]
        public int Id { get; set; }
        [Column(TypeName = "int")][Required]
        public int Id_pessoa { get; set; }
        [Column(TypeName = "nvarchar(40)")][Required][MinLength(3)][MaxLength(40)]
        public string Name { get; set; }
        [Column(TypeName = "nvarchar(200)")][Required][MinLength(10)][MaxLength(200)]
        public string Descricao { get; set; }
        [Column(TypeName = "nvarchar(15)")][Required][MinLength(3)][MaxLength(15)]
        public string Tipo { get; set; }
        [Column(TypeName = "money")][Required]
        public double Preco { get; set; }
        [Column(TypeName = "nvarchar(60)")][Required][MinLength(2)][MaxLength(60)]
        public string Endereco { get; set; }
        [Column(TypeName = "nvarchar(20)")][Required][MinLength(2)][MaxLength(20)]
        public string Bairro { get; set; }
        [Column(TypeName = "int")][Required]
        public int Numero { get; set; }
        [Column(TypeName = "int")][Required]
        public int CEP { get; set; }
        [Column(TypeName = "nvarchar(2)")][Required][MinLength(2)][MaxLength(2)]
        public string UF { get; set; }
        [Column(TypeName = "nvarchar(20)")][Required][MinLength(3)][MaxLength(20)]
        public string Cidade { get; set; }
        [Column(TypeName = "nvarchar(40)")][Required][MinLength(3)][MaxLength(40)]
        public string NomeContato { get; set; }
        [Column(TypeName = "nvarchar(13)")][Required][MinLength(10)][MaxLength(13)]
        public string TelefoneContato { get; set; }
        [Column(TypeName = "nvarchar(40)")][Required][MinLength(3)][MaxLength(40)]
        public string EmailContato { get; set; }
        [Column(TypeName = "datetime")][Required]
        public DateTime DataCadastro { get; set; }
        public Pessoa Pessoa { get; set; }


        /*
        // Validação - verifica se os campos foram preenchidos corretamente
        public string Validacao(Moradia moradia)
        {
            var acumulador = "valido";
            if (moradia.Name == null || moradia.Name.Length < 5) { acumulador = "invalido"; }
            if (moradia.Descricao == null || moradia.Descricao.Length < 10 || moradia.Descricao.Length > 200) { acumulador = "invalido"; }
            if (moradia.Tipo == "-" || moradia.Tipo == null || (moradia.Tipo != "Casa" && moradia.Tipo != "Apartamento" && moradia.Tipo != "Quarto")){ acumulador = "invalido"; }
            if (moradia.Endereco == null || moradia.Endereco.Length > 60) { acumulador = "invalido"; }
            if (moradia.Bairro == null || moradia.Bairro.Length > 20) { acumulador = "invalido"; }
            if (moradia.UF == null || moradia.UF.Length != 2) { acumulador = "invalido"; }
            if (moradia.Cidade == null || moradia.Cidade.Length > 20) { acumulador = "invalido"; }
            if (moradia.NomeContato == null || moradia.NomeContato.Length > 40) { acumulador = "invalido"; }
            if (moradia.TelefoneContato == null || moradia.TelefoneContato.Length > 13) { acumulador = "invalido"; }
            if (moradia.EmailContato == null || moradia.EmailContato.Length > 40) { acumulador = "invalido"; }
          
            return acumulador;
        }
        */
    }
}
