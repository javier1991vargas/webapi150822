using System.ComponentModel.DataAnnotations;

namespace WebApAutores.DTOs
{
    public class AutorCreacionDTOs
    {
        [Required]
        [StringLength(maximumLength: 120, ErrorMessage = "El campo {0} no debe tener mas de {1} caracteres ")]    
        public string Nombre { get; set; }
    }
}
