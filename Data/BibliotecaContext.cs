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
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Prestamo> Prestamos { get; set; }

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

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.Property(u => u.Username)
                    .IsRequired() 
                    .HasMaxLength(200);
                    
                entity.Property(u => u.Nombre)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(u => u.Email)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.HasIndex(u => u.Username)
                    .IsUnique();
                entity.HasIndex(u => u.Email)
                    .IsUnique();

                entity.Property(u => u.Password)
                    .IsRequired();

                entity.Property(u => u.Telefono)
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Prestamo>(entity =>
            {
                entity.HasOne(p => p.Usuario)
                    .WithMany(u => u.Prestamos)
                    .HasForeignKey(p => p.UsuarioId);

                entity.HasOne(p => p.Libro)
                    .WithMany()
                    .HasForeignKey(p => p.LibroId);

                entity.Property(p => p.FechaPrestamo)
                    .IsRequired();
            });
        }
    }
}
