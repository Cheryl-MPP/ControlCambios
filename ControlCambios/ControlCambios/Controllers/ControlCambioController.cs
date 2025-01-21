using ControlCambios.Models;
using ControlCambios.SQL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace ControlCambios.Controllers
{
    public class ControlCambioController : Controller
    {
        private readonly IConfiguration _configuration;

        public ControlCambioController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult ObtenerVersiones(int IdBaseDatos)
        {
            var sql3 = new TablaVersionesBaseDatosSQL(_configuration);
            var lista3 = sql3.ObtenerVersionesBaseDatos(IdBaseDatos);
            if (lista3.Count == 0)
            {
                return Json("");
            }
            else
            {
                return Json(new SelectList(lista3));
            }
        }

        [HttpPost]
        public IActionResult ObtenerCambios(int IdBaseDatos, string NombreVersion)
        {

            if (NombreVersion == null)
            {
                return Json("");
            }

            var sql3 = new TablaVersionesBaseDatosSQL(_configuration);
            var version = sql3.ObtenerVersionBaseDatosPorId(IdBaseDatos, NombreVersion);

            if (version == null)
            {
                return Json("");
            }
            else
            {
                return Json(version.CambiosSolicitados);
            }

        }

       [Authorize(Roles = "Administrador, Usuario")]
        public ActionResult Index()
        {
            var sql = new TablaControlCambioSQL(_configuration);
            return View(sql.ObtenerControlesCambio());
        }

       [Authorize(Roles = "Administrador, Usuario")]
        public ActionResult Details(int id)
        {
            var sql = new TablaControlCambioSQL(_configuration);
            return View(sql.ObtenerControlCambioPorId(id));
        }

       [Authorize(Roles = "Administrador, Usuario")]
        public ActionResult Create()
        {
            var Identificacion = User.FindFirstValue(ClaimTypes.Name);
            ViewBag.Identificacion = Identificacion;


            var sql2 = new TablaBaseDatosSQL(_configuration);
            var lista = sql2.ObtenerBasesDeDatosActivas();
            ViewBag.BaseDatos = new SelectList(lista, "IdBaseDatos", "");
            return View();
        }

       [Authorize(Roles = "Administrador, Usuario")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TablaControlCambio cambio)
        {
            try
            {
                var sql = new TablaControlCambioSQL(_configuration);
                sql.AgregarControlCambio(cambio);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                var Identificacion = User.FindFirstValue(ClaimTypes.Name);
                ViewBag.Identificacion = Identificacion;
                var sql2 = new TablaBaseDatosSQL(_configuration);
                var lista = sql2.ObtenerBasesDeDatosActivas();
                ViewBag.BaseDatos = new SelectList(lista, "IdBaseDatos", "");
                return View();
            }
        }

       [Authorize(Roles = "Administrador, Usuario")]
        public ActionResult Edit(int id)
        {
            var Identificacion = User.FindFirstValue(ClaimTypes.Name);
            ViewBag.Identificacion = Identificacion;

            var sql2 = new TablaBaseDatosSQL(_configuration);
            var lista = sql2.ObtenerBasesDeDatosActivas();
            ViewBag.BaseDatos = new SelectList(lista, "IdBaseDatos", "");

            var sql = new TablaControlCambioSQL(_configuration);
            return View(sql.ObtenerControlCambioPorId(id));
        }

       [Authorize(Roles = "Administrador, Usuario")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TablaControlCambio cambio)
        {
            try
            {
                var sql = new TablaControlCambioSQL(_configuration);
                sql.ActualizarControlCambio(cambio);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                var Identificacion = User.FindFirstValue(ClaimTypes.Name);
                ViewBag.Identificacion = Identificacion;

                var sql2 = new TablaBaseDatosSQL(_configuration);
                var lista = sql2.ObtenerBasesDeDatosActivas();
                ViewBag.BaseDatos = new SelectList(lista, "IdBaseDatos", "");

                var sql = new TablaControlCambioSQL(_configuration);
                return View(sql.ObtenerControlCambioPorId(id));
            }
        }

       [Authorize(Roles = "Administrador, Usuario")]
        public ActionResult Delete(int id)
        {
            var sql = new TablaControlCambioSQL(_configuration);
            return View(sql.ObtenerControlCambioPorId(id));
        }

       [Authorize(Roles = "Administrador, Usuario")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, TablaControlCambio cambio)
        {
            try
            {
                var sql = new TablaControlCambioSQL(_configuration);
                sql.EliminarControlCambio(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.cabecera = "Eliminación Denegada";
                ViewBag.mensaje = "Verifique las dependencias en el dato a eliminar";
                var sql = new TablaControlCambioSQL(_configuration);
                return View(sql.ObtenerControlCambioPorId(id));
            }
        }
    }
}