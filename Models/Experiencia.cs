using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Andor.Models
{
    public class Experiencia
    {
        [Key] [Column(TypeName = "int")]
        public int Id { get; set; }
        [Column(TypeName = "int")]
        public int Id_pessoa { get; set; }
        [Column(TypeName = "nvarchar(40)")]
        public string Nome { get; set; }
        [Column(TypeName = "nvarchar(200)")]
        public string Descricao { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string Cargo { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string Instituicao { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "datetime")]
        public DateTime? Inicio { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "datetime")]
        public DateTime? Fim { get; set; }
        public Pessoa Pessoa { get; set; }
    }
}
