using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TiendaVirtual.Data;
using TiendaVirtual.Helpers;
using TiendaVirtual.Models;

namespace TiendaVirtual.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly TiendaContext _context;

        public UsuariosController(TiendaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("Usuario") == null)
                return RedirectToAction("Index", "Login");

            return View(await _context.Usuarios.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("Usuario") == null)
                return RedirectToAction("Index", "Login");

            if (id == null) return NotFound();
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null) return NotFound();
            return View(usuario);
        }

        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("Usuario") == null)
                return RedirectToAction("Index", "Login");

            ViewBag.Roles = new SelectList(new[] { "administrador", "cliente" });
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.Contraseña = HashHelper.ObtenerHash(usuario.Contraseña);
                _context.Usuarios.Add(usuario);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Roles = new SelectList(new[] { "administrador", "cliente" });
            return View(usuario);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("Usuario") == null)
                return RedirectToAction("Index", "Login");

            if (id == null) return NotFound();
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return NotFound();

            // Limpiar contraseña para no mostrar el hash
            usuario.Contraseña = string.Empty;
            ViewBag.Roles = new SelectList(new[] { "administrador", "cliente" }, usuario.Rol);
            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Usuario usuario)
        {
            if (id != usuario.Id) return NotFound();

            if (ModelState.IsValid)
            {
                // Si dejó la contraseña vacía, conservar la anterior
                if (string.IsNullOrWhiteSpace(usuario.Contraseña))
                {
                    var contraActual = await _context.Usuarios.AsNoTracking()
                        .Where(u => u.Id == id)
                        .Select(u => u.Contraseña)
                        .FirstOrDefaultAsync();
                    usuario.Contraseña = contraActual ?? string.Empty;
                }
                else
                {
                    usuario.Contraseña = HashHelper.ObtenerHash(usuario.Contraseña);
                }

                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Usuarios.Any(e => e.Id == usuario.Id))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Roles = new SelectList(new[] { "administrador", "cliente" }, usuario.Rol);
            return View(usuario);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("Usuario") == null)
                return RedirectToAction("Index", "Login");

            if (id == null) return NotFound();
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null) return NotFound();
            return View(usuario);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null) _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
