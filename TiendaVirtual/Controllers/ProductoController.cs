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

        public async Task<IActionResult> Index()
        {
            var productos = await _context.Productos
                .Include(p => p.Categoria)
                .ToListAsync();
            return View(productos);


        }
        //formulario crear
        public IActionResult Create()
        {
            return View();
        }

        //guardar
        [HttpPost]
        public IActionResult Create(Producto producto)
        {
            _context.Productos.Add(producto);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        //formulario editar
        public IActionResult Edit(int id)
        {
            var producto = _context.Productos.Find(id);
            ViewBag.Categorias = _context.Categorias.ToList();

            return View(producto);

        }

        //actualizar
        [HttpPost]
        public IActionResult Edit(Producto producto)
        {
            _context.Productos.Update(producto);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        //eliminar
        public IActionResult Delete(int id)
        {
            var producto = _context.Productos.Find(id);

            _context.Productos.Remove(producto);
            _context.SaveChanges();

            return RedirectToAction("Index"); 
        }

        // GET
        public IActionResult Create()
        {
            ViewBag.CategoriaId = new SelectList(_context.Categorias, "Id", "Nombre");
            return View();
        }

        // POST
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

        // GET
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null) return NotFound();

            ViewBag.CategoriaId = new SelectList(_context.Categorias, "Id", "Nombre", producto.CategoriaId);
            return View(producto);
        }

        // POST
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
    }
}


