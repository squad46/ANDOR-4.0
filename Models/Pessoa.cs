using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Andor.Models
{

    public class Pessoa
    {
        [Key][Column(TypeName = "int")]
        public int Id { get; set; }
        [Required][Column(TypeName = "nvarchar(40)")]
        public string Nome { get; set; }
        [Required][Column(TypeName = "nvarchar(40)")]
        public string Email { get; set; }
        [Required][Column(TypeName = "nvarchar(60)")]
        public string Senha { get; set; }
        [Column(TypeName = "nvarchar(13)")]
        public string Telefone { get; set; }
        [Column(TypeName = "nvarchar(15)")]
        public string CRNM { get; set; }
        [Column(TypeName = "nvarchar(11)")]
        public string CPF { get; set; }
        [Column(TypeName = "nvarchar(60)")]
        public string Endereco { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string Bairro { get; set; }
        [Column(TypeName = "int")]
        public int Numero { get; set; }
        [Column(TypeName = "int")]
        public int CEP { get; set; }
        [Column(TypeName = "nvarchar(2)")]
        public string UF { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string Cidade { get; set; }
        [Column(TypeName = "nvarchar(1)")]
        public string Sexo { get; set; }
        [DataType(DataType.Date)][Column(TypeName = "datetime")]
        public DateTime? DataNascimento { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string Nacionalidade { get; set; }
        [DataType(DataType.Date)][Column(TypeName = "datetime")]
        public DateTime? DataCadastro { get; set; }
        [Column(TypeName = "nvarchar(10)")]
        public string Classe { get; set; }
   

        public ICollection<Formacao> Formacoes { get; set; }
        public ICollection<Experiencia> Experiencias { get; set; }
        public ICollection<Moradia> Moradias { get; set; }
        public ICollection<Trabalho> Trabalhos { get; set; }


        // Verifica se o usuario completou seu cadastro e faz lista de campos a serem preenchidos
        public string VerificaCadastroCompleto(Pessoa pessoa)
        {
            var camposVazios = "Por favor complete seu cadastro preenchendo os seguintes campos";
            var acumulador = 0;
            if (pessoa.Telefone == null) { acumulador++; camposVazios = camposVazios + ", Telefone"; }
            if (pessoa.CRNM == null) { acumulador++; camposVazios = camposVazios + ", CRNM"; }
            if (pessoa.CPF == null) { acumulador++; camposVazios = camposVazios + ", CPF"; }
            if (pessoa.Endereco == null) { acumulador++; camposVazios = camposVazios + ", Endereço"; }
            if (pessoa.Bairro == null) { acumulador++; camposVazios = camposVazios + ", Bairro"; }
            if (pessoa.Numero == 0) { acumulador++; camposVazios = camposVazios + ", Número"; }
            if (pessoa.CEP == 0) { acumulador++; camposVazios = camposVazios + ", CEP"; }
            if (pessoa.UF == "UF") { acumulador++; camposVazios = camposVazios + ", UF"; }
            if (pessoa.Cidade == null) { acumulador++; camposVazios = camposVazios + ", Cidade"; }
            if (pessoa.Sexo == null) { acumulador++; camposVazios = camposVazios + ", Sexo"; }
            if (pessoa.DataNascimento == null) { acumulador++; camposVazios = camposVazios + ", Data de nascimento"; }
            camposVazios = camposVazios + ".";

            if (acumulador > 0)
            {
                return camposVazios;
            }
            else
            {
                return null;
            }
        }
    }
}
