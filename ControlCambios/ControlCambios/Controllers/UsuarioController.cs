using ControlCambios.Models;
using ControlCambios.SQL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ControlCambios.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IConfiguration _configuration;

        public UsuarioController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Authorize(Roles = "Administrador")]
        public ActionResult Index()
        {
            var sql = new TablaUsuarioSQL(_configuration);
            return View(sql.ObtenerUsuarios());
        }

        [Authorize(Roles = "Administrador")]
        public ActionResult Details(string id)
        {
            var sql = new TablaUsuarioSQL(_configuration);
            return View(sql.ObtenerUsuarioPorId(id));
        }

        [Authorize(Roles = "Administrador")]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TablaUsuario usuario)
        {
            try
            {
                var sql = new TablaUsuarioSQL(_configuration);
                sql.AgregarUsuario(usuario);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "Administrador")]
        public ActionResult Edit(string id)
        {
            var sql = new TablaUsuarioSQL(_configuration);
            return View(sql.ObtenerUsuarioPorId(id));
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, TablaUsuario usuario)
        {
            try
            {
                var sql = new TablaUsuarioSQL(_configuration);
                sql.ActualizarUsuario(usuario);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                var sql = new TablaUsuarioSQL(_configuration);
                return View(sql.ObtenerUsuarioPorId(id));
            }
        }

        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(string id)
        {
            var sql = new TablaUsuarioSQL(_configuration);
            return View(sql.ObtenerUsuarioPorId(id));
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, TablaUsuario usuario)
        {
            try
            {
                var sql = new TablaUsuarioSQL(_configuration);
                sql.EliminarUsuario(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.cabecera = "Eliminación Denegada";
                ViewBag.mensaje = "Verifique las dependencias en el dato a eliminar";
                var sql = new TablaUsuarioSQL(_configuration);
                return View(sql.ObtenerUsuarioPorId(id));
            }
        }
    }
}