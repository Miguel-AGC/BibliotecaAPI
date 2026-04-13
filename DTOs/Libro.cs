namespace BibliotecaAPI.DTOs
{
    public class CrearLibroDTO
    {
        public string Titulo { get; set; }
        public string ISBN { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public int Ejemplares { get; set; }
        public int AutorId { get; set; }
    }

    public class LibroDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string ISBN { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public int Ejemplares { get; set; }

        public bool Disponible { get; set; }

        public string Autor { get; set; }
    }

    public class ActualizarLibroDTO
    {
        public string Titulo { get; set; }
        public string ISBN { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public int Ejemplares { get; set; }
        public int AutorId { get; set; }
    }
}
