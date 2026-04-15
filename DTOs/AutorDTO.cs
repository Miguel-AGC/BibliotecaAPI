using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.DTOs
{
    public class AutorDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public DateTime FechaNacimiento { get; set; }
        public string Nacionalidad { get; set; }
    }

    public class CrearAutorDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        public DateTime FechaNacimiento { get; set; }
        [MinLength(3)]
        [MaxLength(50)]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$")]
        public string Nacionalidad { get; set; }
    }
}
