using System.ComponentModel.DataAnnotations;

namespace TiendaVirtual.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(200)]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; } = string.Empty;

        public List<Producto>? Productos { get; set; }
    }
}
