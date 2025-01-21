using ControlCambios.Models;
using ControlCambios.SQL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ControlCambios.Controllers
{
    public class BaseDatosController : Controller
    {
        private readonly IConfiguration _configuration;

        public BaseDatosController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

       [Authorize(Roles = "Administrador, Usuario")]
        public ActionResult Index()
        {
            var sql = new TablaBaseDatosSQL(_configuration);
            return View(sql.ObtenerBasesDeDatosTodas());
        }


       [Authorize(Roles = "Administrador, Usuario")]
        public ActionResult Details(int id)
        {
            var sql = new TablaBaseDatosSQL(_configuration);
            return View(sql.ObtenerBaseDatosPorId(id));
        }

       [Authorize(Roles = "Administrador, Usuario")]
        public ActionResult Create()
        {
            return View();
        }

       [Authorize(Roles = "Administrador, Usuario")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TablaBaseDatos collection)
        {
            try
            {
                var sql = new TablaBaseDatosSQL(_configuration);
                sql.AgregarBaseDatos(collection);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

       [Authorize(Roles = "Administrador, Usuario")]
        public ActionResult Edit(int id)
        {
            var sql = new TablaBaseDatosSQL(_configuration);
            return View(sql.ObtenerBaseDatosPorId(id));
        }

       [Authorize(Roles = "Administrador, Usuario")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TablaBaseDatos collection)
        {
            try
            {
                var sql = new TablaBaseDatosSQL(_configuration);
                sql.ActualizarBaseDatos(collection);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                var sql = new TablaBaseDatosSQL(_configuration);
                return View(sql.ObtenerBaseDatosPorId(id));
            }
        }

       [Authorize(Roles = "Administrador, Usuario")]
        public ActionResult Delete(int id)
        {
            var sql = new TablaBaseDatosSQL(_configuration);
            return View(sql.ObtenerBaseDatosPorId(id));
        }

       [Authorize(Roles = "Administrador, Usuario")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, TablaBaseDatos collection)
        {
            try
            {
                var sql = new TablaBaseDatosSQL(_configuration);
                sql.EliminarBaseDatos(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.cabecera = "Eliminación Denegada";
                ViewBag.mensaje = "Verifique las dependencias en el dato a eliminar";
                var sql = new TablaBaseDatosSQL(_configuration);
                return View(sql.ObtenerBaseDatosPorId(id));
            }
        }
    }
}