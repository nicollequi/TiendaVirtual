using System.ComponentModel.DataAnnotations;

namespace TiendaVirtual.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "Correo inválido")]
        public string Correo { get; set; } = string.Empty;

        [Required]
        public string Rol { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^3\d{9}$",
            ErrorMessage = "El celular debe estar entre 3000000000 y 3999999999")]
        public string Celular { get; set; } = string.Empty;

        [Required]
        [MinLength(4, ErrorMessage = "Mínimo 4 caracteres")]
        public string Contraseña { get; set; } = string.Empty;
    }
}