namespace BibliotecaAPI.DTOs
{
    public class CrearPrestamoDTO
    {
        public int UsuarioId { get; set; }
        public int LibroId { get; set; }
    }

    public class PrestamoDTO
    {
        public int Id { get; set; }

        public string UsuarioNombre { get; set; }
        public string LibroTitulo { get; set; }

        public DateTime FechaPrestamo { get; set; }
        public DateTime? FechaDevolucion { get; set; }

        public bool Activo { get; set; }
    }
}
