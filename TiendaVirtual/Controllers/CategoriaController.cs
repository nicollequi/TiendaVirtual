using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaVirtual.Data;
using TiendaVirtual.Models;

namespace TiendaVirtual.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly TiendaContext _context;

        public CategoriaController(TiendaContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var categorias = _context.Categorias.Include(c => c.Productos).ToList();
            return View(categorias);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _context.Categorias.Add(categoria);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        public IActionResult Edit(int id)
        {
            var categoria = _context.Categorias.Find(id);
            if (categoria == null) return NotFound();
            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Categoria categoria)
        {
            if (id != categoria.Id) return NotFound();
            if (ModelState.IsValid)
            {
                _context.Categorias.Update(categoria);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        public IActionResult Delete(int id)
        {
            var categoria = _context.Categorias.Find(id);
            if (categoria == null) return NotFound();
            return View(categoria);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var categoria = _context.Categorias.Find(id);
            if (categoria != null)
                _context.Categorias.Remove(categoria);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
