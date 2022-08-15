namespace WebApAutores.DTOs
{
    public class LibroDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public DateTime FechaDePublicacion { get; set; }
        public List<ComentarioDTO> comentarios { get; set; }
        public List<AutorDTO> Aut_ores { get; set; }
    }
}
