namespace BibliotecaAPI.Models
{
    public class Prestamo
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public int LibroId { get; set; }
        public Libro Libro { get; set; }

        public DateTime FechaPrestamo { get; set; } = DateTime.Now;

        public DateTime? FechaDevolucion { get; set; }

        public bool Activo => FechaDevolucion == null;
    }
}
