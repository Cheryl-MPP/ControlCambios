using ControlCambios.Models;
using ControlCambios.SQL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ControlCambios.Controllers
{
    public class VersionesBaseDatosController : Controller
    {
        private readonly IConfiguration _configuration;

        public VersionesBaseDatosController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

       [Authorize(Roles = "Administrador, Usuario")]
        public ActionResult Index()
        {
            var sql = new TablaVersionesBaseDatosSQL(_configuration);
            return View(sql.ObtenerVersionesBaseDatos());
        }

       [Authorize(Roles = "Administrador, Usuario")]
        public ActionResult Details(int id, string NombreVersion)
        {
            var sql = new TablaVersionesBaseDatosSQL(_configuration);
            return View(sql.ObtenerVersionBaseDatosPorId(id, NombreVersion));
        }

       [Authorize(Roles = "Administrador, Usuario")]
        public ActionResult Create()
        {
            var sql2 = new TablaBaseDatosSQL(_configuration);
            ViewBag.BaseDatos = new SelectList(sql2.ObtenerBasesDeDatosActivas(), "IdBaseDatos", "");

            return View();
        }

       [Authorize(Roles = "Administrador, Usuario")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TablaVersionesBaseDatos version)
        {
            try
            {
                var sql = new TablaVersionesBaseDatosSQL(_configuration);
                sql.AgregarVersionBaseDatos(version);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.cabecera = "Inserción Denegada";
                ViewBag.mensaje = "Primero debe registrar una Base de datos";

                var sql2 = new TablaBaseDatosSQL(_configuration);
                ViewBag.BaseDatos = new SelectList(sql2.ObtenerBasesDeDatosActivas(), "IdBaseDatos", "");

                return View();
            }
        }

       [Authorize(Roles = "Administrador, Usuario")]
        public ActionResult Edit(int id, string NombreVersion)
        {
            var sql2 = new TablaBaseDatosSQL(_configuration);
            ViewBag.BaseDatos = new SelectList(sql2.ObtenerBasesDeDatosActivas(), "IdBaseDatos", "");

            var sql = new TablaVersionesBaseDatosSQL(_configuration);
            return View(sql.ObtenerVersionBaseDatosPorId(id, NombreVersion));
        }

       [Authorize(Roles = "Administrador, Usuario")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, string NombreVersion, TablaVersionesBaseDatos version)
        {
            try
            {
                var sql = new TablaVersionesBaseDatosSQL(_configuration);
                sql.ActualizarVersionBaseDatos(version);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                var sql2 = new TablaBaseDatosSQL(_configuration);
                ViewBag.BaseDatos = new SelectList(sql2.ObtenerBasesDeDatosActivas(), "IdBaseDatos", "");

                ViewBag.cabecera = "Actualización Denegada";
                ViewBag.mensaje = "Verifique las dependencias en el dato a actualizar";

                var sql = new TablaVersionesBaseDatosSQL(_configuration);
                return View(sql.ObtenerVersionBaseDatosPorId(id, NombreVersion));
            }
        }

       [Authorize(Roles = "Administrador, Usuario")]
        public ActionResult Delete(int id, string NombreVersion)
        {
            var sql = new TablaVersionesBaseDatosSQL(_configuration);
            return View(sql.ObtenerVersionBaseDatosPorId(id, NombreVersion));
        }

       [Authorize(Roles = "Administrador, Usuario")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, string NombreVersion, TablaVersionesBaseDatos version)
        {
            try
            {
                var sql = new TablaVersionesBaseDatosSQL(_configuration);
                sql.EliminarVersionBaseDatos(id, NombreVersion);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.cabecera = "Eliminación Denegada";
                ViewBag.mensaje = "Verifique las dependencias en el dato a eliminar";
                var sql = new TablaVersionesBaseDatosSQL(_configuration);
                return View(sql.ObtenerVersionBaseDatosPorId(id, NombreVersion));
            }
        }
    }
}