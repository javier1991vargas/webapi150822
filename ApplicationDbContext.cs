using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApAutores.Entidades;

namespace WebApAutores
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AutoresLibros>()
                .HasKey(al => new {al.AutorId, al.LibroId });
        }
        public DbSet<Autor> Autores{ get; set; }
        public DbSet<Libro> Libros { get; set; }
        public DbSet<Comentarios> Comentarios { get; set; }
        public DbSet<AutoresLibros> AutoresLibros { get; set; }
    }
}
