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

        // LISTAR
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Usuario") == null)
                return RedirectToAction("Index", "Login");

            var categorias = _context.Categorias.ToList();
            return View(categorias);
        }

        // FORMULARIO CREAR
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("Usuario") == null)
                return RedirectToAction("Index", "Login");

            return View();
        }

        // GUARDAR
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

        // FORMULARIO EDITAR
        public IActionResult Edit(int id)
        {
            if (HttpContext.Session.GetString("Usuario") == null)
                return RedirectToAction("Index", "Login");

            var categoria = _context.Categorias.Find(id);
            if (categoria == null) return NotFound();
            return View(categoria);
        }

        // ACTUALIZAR
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

        // CONFIRMAR ELIMINAR
        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("Usuario") == null)
                return RedirectToAction("Index", "Login");

            var categoria = _context.Categorias.Find(id);
            if (categoria == null) return NotFound();
            return View(categoria);
        }

        // ELIMINAR
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var categoria = _context.Categorias.Find(id);
            _context.Categorias.Remove(categoria);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}