using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TiendaVirtual.Data;
using TiendaVirtual.Helpers;
using TiendaVirtual.Models;

namespace TiendaVirtual.Controllers
{
    public class UsuarioController : Controller
    {

        private readonly TiendaContext _context;
        public UsuarioController(TiendaContext context)
        {
            _context = context;
        }

        // GET: UsusarioController
        public ActionResult Index()
        {
            var usuarios = _context.Usuarios
            .ToList();

            return View(usuarios);
        }


        // GET: UsusarioController/Create
        public ActionResult Create()
        {
            ViewBag.Usuarios = _context.Usuarios.ToList();
            return View();
        }

        // POST: UsusarioController/Create
        [HttpPost]
        public ActionResult Create(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.Contraseña = HashHelper.ObtenerHash(usuario.Contraseña);

                _context.Usuarios.Add(usuario);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(usuario);
        }
    }
}