using BibliotecaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Data
{
    public class BibliotecaContext : DbContext
    {
        public BibliotecaContext(DbContextOptions<BibliotecaContext> options) : base(options)
        {
        }

        public DbSet<Autor> Autores { get; set; }
        public DbSet<Libro> Libros { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configuración de la relación entre Autor y Libro
            modelBuilder.Entity<Libro>(entity =>
            {
                entity.Property(l => l.Titulo)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(l => l.ISBN)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasIndex(l => l.ISBN)
                    .IsUnique(); // Asegura que el ISBN sea único

                entity.Property(l => l.Ejemplares)
                    .IsRequired();

                entity.HasOne(l => l.Autor)
                    .WithMany(a => a.Libros)
                    .HasForeignKey(l => l.AutorId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
