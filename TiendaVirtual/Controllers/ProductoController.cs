using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TiendaVirtual.Data;
using TiendaVirtual.Models;

namespace TiendaVirtual.Controllers
{
    public class ProductoController : Controller
    {
        private readonly TiendaContext _context;

        public ProductoController(TiendaContext context)
        {
            _context = context;
        }

        // METODO CREAR
        [HttpPost]
        public IActionResult Create(Producto producto, IFormFile imagen)
        {
            if (imagen != null)
            {
                var ruta = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot/images", imagen.FileName);

                using (var stream = new FileStream(ruta, FileMode.Create))
                {
                    imagen.CopyTo(stream);
                }

                producto.ImagenUrl = "/images/" + imagen.FileName;
            }

            _context.Productos.Add(producto);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // LISTAR PRODUCTOS
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Usuario") == null)
                return RedirectToAction("Index", "Login");

            var productos = _context.Productos
                .Include(p => p.Categoria)
                .ToList();
            return View(productos);
        }

        // FORMULARIO CREAR
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("Usuario") == null)
                return RedirectToAction("Index", "Login");

            ViewBag.CategoriaId = new SelectList(_context.Categorias, "Id", "Nombre");
            return View();
        }

        // GUARDAR
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Producto producto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.CategoriaId = new SelectList(_context.Categorias, "Id", "Nombre");
            return View(producto);
        }

        // FORMULARIO EDITAR
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("Usuario") == null)
                return RedirectToAction("Index", "Login");

            if (id == null) return NotFound();
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null) return NotFound();
            ViewBag.CategoriaId = new SelectList(_context.Categorias, "Id", "Nombre", producto.CategoriaId);
            return View(producto);
        }

        // ACTUALIZAR
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Producto producto)
        {
            if (id != producto.Id) return NotFound();
            if (ModelState.IsValid)
            {
                _context.Update(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.CategoriaId = new SelectList(_context.Categorias, "Id", "Nombre", producto.CategoriaId);
            return View(producto);
        }

        // CONFIRMAR ELIMINAR
        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("Usuario") == null)
                return RedirectToAction("Index", "Login");

            var rol = HttpContext.Session.GetString("Rol");

            // SOLO ADMIN PUEDE ELIMINAR
            if (rol != "administrador")
                return RedirectToAction("Index");

            var producto = _context.Productos.Find(id);
            if (producto == null) return NotFound();
            return View(producto);
        }

        // ELIMINAR
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var producto = _context.Productos.Find(id);
            _context.Productos.Remove(producto);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}