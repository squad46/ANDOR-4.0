using System.ComponentModel.DataAnnotations;

namespace Andor.Models
{
    public class Imagem
    {
        [Key]
        public int Id { get; set; }
        public int Id_tipo { get; set; }
        public string Tipo { get; set; } 
        public string Nome { get; set; }
        public byte[] Dados { get; set; }
        public string ContentType { get; set; }
    }
}
