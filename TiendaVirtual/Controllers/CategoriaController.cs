using Microsoft.AspNetCore.Mvc;
using TiendaVirtual.Models;

namespace TiendaVirtual.Controllers
{
    public class CategoriaController : Controller
    {
        public IActionResult Index()
        {
            var categorias = new List<Categoria>
            {
                new Categoria { Id = 1, Nombre = "Electrónica", Descripcion = "Productos electrónicos" },
                new Categoria { Id = 2, Nombre = "Ropa", Descripcion = "Prendas de vestir" },
                new Categoria { Id = 3, Nombre = "", Descripcion = "Categoría sin nombre" },
                new Categoria { Id = 4, Nombre = null, Descripcion = "Otra sin nombre" }
            };

            return View(categorias);
        }
    }
}
