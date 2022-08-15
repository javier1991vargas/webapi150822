namespace WebApAutores.DTOs
{
    public class LibropatchDto
    {
        public string Titulo { get; set; }
        public DateTime? FechaDePublicacion { get; set; }
        public List<int> Autoreids { get; set; }

    }
}
