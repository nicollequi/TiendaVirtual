using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TiendaVirtual.Data;
using TiendaVirtual.Helpers;
using TiendaVirtual.Models;

namespace TiendaVirtual.Controllers
{
    public class LoginController : Controller
    {
        private readonly TiendaContext _context;

        public LoginController(TiendaContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(string correo, string clave)
        {
            // Limpiar espacios accidentales
            correo = correo?.Trim() ?? "";
            clave = clave?.Trim() ?? "";

            string claveHash = HashHelper.ObtenerHash(clave);

            var usuario = _context.Usuarios
                .FirstOrDefault(u => u.Correo.Trim() == correo && u.Contraseña == claveHash);

            if (usuario != null)
            {
                HttpContext.Session.SetString("Usuario", usuario.Nombre);
                HttpContext.Session.SetString("Rol", usuario.Rol);
                return RedirectToAction("Index", "Home");
            }

            // Verificar si el correo existe pero la clave es incorrecta
            var usuarioExiste = _context.Usuarios.FirstOrDefault(u => u.Correo.Trim() == correo);
            if (usuarioExiste == null)
                ViewBag.Error = "El correo no está registrado.";
            else
                ViewBag.Error = "Contraseña incorrecta. Verifica tu clave.";

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
