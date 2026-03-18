using System.ComponentModel.DataAnnotations;

namespace TiendaVirtual.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [StringLength(200)]
        public string Descripcion { get; set; }  // ← agrega esta línea

        public List<Producto> Productos { get; set; }
    }
}