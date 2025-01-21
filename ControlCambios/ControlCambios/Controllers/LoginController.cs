using ControlCambios.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ControlCambios.SQL;

namespace ControlCambios.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult AccessDenied()
        {
            return View();
        }


        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            if (ModelState.IsValid)
            {
                var sql1 = new LoginSQL(_configuration);
                bool administrador = sql1.AccederRol(login, "Administrador");

                if (administrador)
                {
                    crearCredenciales("Administrador", login);
                    return RedirectToAction("Index", "Home"); 
                }
                else
                {
                    bool usuario = sql1.AccederRol(login, "Usuario");
                    if (usuario)
                    {
                        crearCredenciales("Usuario", login);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.cabecera = "Accesso Denegado";
                        ViewBag.mensaje = "Verifique su identificación y contraseña";
                        return View(login);
                    }
                }
            }

            return View(login);
        }

        private async void crearCredenciales(String rol, Login login)
        {
            var sql1 = new LoginSQL(_configuration);

            List<Claim> claims = new List<Claim>
                    {
                           new Claim(ClaimTypes.Role, rol),
                           new Claim(ClaimTypes.Authentication, login.Usuario),
                           new Claim(ClaimTypes.Name, sql1.IdUsuario(login)),

                    };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties
            {
                IsPersistent = false,
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, properties);

        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Login");
        }
    }

}