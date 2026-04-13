namespace BibliotecaAPI.Models
{
    public class Libro
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        
        // Relación
        public int AutorId { get; set; }
        public Autor Autor { get; set; }
        public string ISBN { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public int Ejemplares { get; set; }
        public bool Disponible => Ejemplares > 0;

        //public ICollection<Prestamo> Prestamos { get; set; }
    }
}
