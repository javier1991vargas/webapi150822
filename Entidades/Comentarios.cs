using Microsoft.AspNetCore.Identity;

namespace WebApAutores.Entidades
{
    public class Comentarios
    {
        public int Id { get; set; }
        public string Contenido { get; set; }
        public int LibroID { get; set; }
        public Libro libro { get; set; }
        public String UsuarioId { get; set; }
        public IdentityUser Usuario { get; set; }

    }
}
