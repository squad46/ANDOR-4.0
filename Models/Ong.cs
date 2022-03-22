using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Andor.Models
{
    public class Ong
    {
        [Key]
        [Column(TypeName = "bigint")]
        public long Id { get; set; }
		[Column(TypeName = "nvarchar(20)")][Required][MinLength(3)][MaxLength(20)]
        public string Nome { get; set; }

		[Column(TypeName = "nvarchar(400)")][Required][MinLength(3)][MaxLength(400)]
        public string Descricao { get; set; }
		[Column(TypeName = "nvarchar(100)")][Required][MinLength(3)][MaxLength(100)]
        public string Site { get; set; }
		[Column(TypeName = "nvarchar(100)")][Required][MinLength(3)][MaxLength(100)]
        public string Imagem { get; set; }
	}
}
