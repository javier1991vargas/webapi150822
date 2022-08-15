using System.ComponentModel.DataAnnotations;

namespace WebApAutores.Entidades
{
    public class Autor
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength:120, ErrorMessage ="El campo {0} no debe tener mas de {1} caracteres ")]
        public string Nombre { get; set; }
        public List<AutoresLibros> AutoresLibros { get; set; }

    }
}
