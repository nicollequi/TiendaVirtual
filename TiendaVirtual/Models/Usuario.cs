using System.ComponentModel.DataAnnotations;

namespace TiendaVirtual.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Entre 3 y 100 caracteres")]
        [Display(Name = "Nombre completo")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "Correo inválido")]
        [Display(Name = "Correo electrónico")]
        public string Correo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El rol es obligatorio")]
        [Display(Name = "Rol")]
        public string Rol { get; set; } = string.Empty;

        [Required(ErrorMessage = "El celular es obligatorio")]
        [RegularExpression(@"^3\d{9}$", ErrorMessage = "Debe tener 10 dígitos y empezar por 3")]
        [Display(Name = "Celular")]
        public string Celular { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [MinLength(4, ErrorMessage = "Mínimo 4 caracteres")]
        [Display(Name = "Contraseña")]
        public string Contraseña { get; set; } = string.Empty;
    }
}
