namespace BibliotecaAPI.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        public string Username { get; set; }
        public string Nombre { get; set; }
        public string Rol { get; set; } = "Usuario";

        public string Email { get; set; }

        public string Password { get; set; }

        public string Telefono { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
    }
}
