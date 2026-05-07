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
        private readonly IWebHostEnvironment _env;

        public ProductoController(TiendaContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Usuario") == null)
                return RedirectToAction("Index", "Login");

            var productos = _context.Productos.Include(p => p.Categoria).ToList();
            return View(productos);
        }

        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("Usuario") == null)
                return RedirectToAction("Index", "Login");

            ViewBag.CategoriaId = new SelectList(_context.Categorias, "Id", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Producto producto, IFormFile? imagen)
        {
            if (HttpContext.Session.GetString("Usuario") == null)
                return RedirectToAction("Index", "Login");

            if (ModelState.IsValid)
            {
                if (imagen != null && imagen.Length > 0)
                    producto.ImagenUrl = await GuardarImagen(imagen);

                _context.Productos.Add(producto);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoriaId = new SelectList(_context.Categorias, "Id", "Nombre", producto.CategoriaId);
            return View(producto);
        }

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Producto producto, IFormFile? imagen)
        {
            if (HttpContext.Session.GetString("Usuario") == null)
                return RedirectToAction("Index", "Login");

            if (ModelState.IsValid)
            {
                var productoBD = _context.Productos.Find(producto.Id);
                if (productoBD == null) return NotFound();

                productoBD.Nombre = producto.Nombre;
                productoBD.Precio = producto.Precio;
                productoBD.Stock = producto.Stock;
                productoBD.CategoriaId = producto.CategoriaId;

                if (imagen != null && imagen.Length > 0)
                    productoBD.ImagenUrl = await GuardarImagen(imagen);

                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoriaId = new SelectList(_context.Categorias, "Id", "Nombre", producto.CategoriaId);
            return View(producto);
        }

        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("Usuario") == null)
                return RedirectToAction("Index", "Login");

            if (HttpContext.Session.GetString("Rol") != "administrador")
                return RedirectToAction("Index");

            var producto = _context.Productos.Find(id);
            if (producto == null) return NotFound();
            return View(producto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var producto = _context.Productos.Find(id);
            if (producto != null) _context.Productos.Remove(producto);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        private async Task<string> GuardarImagen(IFormFile file)
        {
            var carpeta = Path.Combine(_env.WebRootPath, "images");
            Directory.CreateDirectory(carpeta);
            var nombre = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var ruta = Path.Combine(carpeta, nombre);
            using var stream = new FileStream(ruta, FileMode.Create);
            await file.CopyToAsync(stream);
            return $"/images/{nombre}";
        }
    }
}
