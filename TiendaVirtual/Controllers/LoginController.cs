using Microsoft.AspNetCore.Mvc;
using TiendaVirtual.Data;
using TiendaVirtual.Helpers;

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
        public IActionResult Index(string correo, string clave)
        {
            correo = correo?.Trim() ?? "";
            clave = clave?.Trim() ?? "";

            string claveHash = HashHelper.ObtenerHash(clave);

            var usuario = _context.Usuarios
                .FirstOrDefault(u => u.Correo.Trim() == correo && u.Contraseña == claveHash);

            if (usuario != null)
            {
                HttpContext.Session.SetString("Usuario", usuario.Nombre);
                HttpContext.Session.SetString("Rol", usuario.Rol);
                return RedirectToAction("Index", "Home"); // <- va a Home
            }

            var existe = _context.Usuarios.FirstOrDefault(u => u.Correo.Trim() == correo);
            ViewBag.Error = existe == null ? "El correo no está registrado." : "Contraseña incorrecta.";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
