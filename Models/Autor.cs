namespace BibliotecaAPI.Models
{
    public class Autor
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }

        public DateTime FechaNacimiento { get; set; }
        public required string Nacionalidad { get; set; }

        //public ICollection<Libro> Libros { get; set; }
    }
}
