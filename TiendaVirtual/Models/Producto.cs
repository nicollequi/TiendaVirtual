using System.ComponentModel.DataAnnotations;

namespace TiendaVirtual.Models
{
    public class Producto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Range(0.01, 1000000, ErrorMessage = "El precio debe ser mayor a 0")]
        [Display(Name = "Precio")]
        public double Precio { get; set; }

        [Range(0, 1000, ErrorMessage = "El stock debe estar entre 0 y 1000")]
        [Display(Name = "Stock")]
        public int Stock { get; set; }

        [Display(Name = "Imagen")]
        public string? ImagenUrl { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una categoría")]
        [Display(Name = "Categoría")]
        public int CategoriaId { get; set; }

        public Categoria? Categoria { get; set; }

        public double CalcularValorInventario() => Precio * Stock;
        public bool TieneStock() => Stock > 0;
    }
}
