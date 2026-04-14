namespace BibliotecaAPI.DTOs
{
    public class CrearUsuarioDTO
    {
        public string Username { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Telefono { get; set; }
    }

    public class ActualizarUsuarioDTO
    {
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string? Telefono { get; set; }
    }

    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaRegistro { get; set; }
    }

    public class UsuarioFiltroDTO
    {
        public string? Nombre { get; set; }
        public string? Email { get; set; }

        public string? OrdenarPor { get; set; } = "id";
        public bool Desc { get; set; } = false;

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class CambiarPasswordDTO
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
