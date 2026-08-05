using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web.Services;
using static Abstracciones.Modelos.RegistroAcceso.RegistroAcceso;
using static Abstracciones.Modelos.Usuario.Usuario;

namespace Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly UsuarioService _usuarioService;
        private readonly RegistroAccesoService _registroAccesoService;

        public LoginController(UsuarioService usuarioService, RegistroAccesoService registroAccesoService)
        {
            _usuarioService = usuarioService;
            _registroAccesoService = registroAccesoService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginRequest request)
        {
            var usuario = await _usuarioService.Login(request);

            if (usuario == null)
            {
                await _registroAccesoService.Registrar(new RegistrarAccesoRequest
                {
                    Id_Usuario = null,
                    Nombre_Usuario = request.Nombre_Usuario ?? "(desconocido)",
                    Exitoso = false
                });

                ViewBag.Error = "Usuario o contraseña incorrectos.";
                return View(request);
            }

            var idRegistro = await _registroAccesoService.Registrar(new RegistrarAccesoRequest
            {
                Id_Usuario = usuario.Id_Usuario,
                Nombre_Usuario = usuario.Nombre_Usuario,
                Exitoso = true
            });

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, usuario.Id_Usuario.ToString()),
                new(ClaimTypes.Name,           usuario.Nombre_Usuario ?? ""),
                new(ClaimTypes.Role,           usuario.Rol ?? ""),
                new("NombreTrabajador",        usuario.Nombre_Trabajador ?? ""),
                new("Id_Trabajador",           usuario.Id_Trabajador.ToString())
            };

            if (idRegistro.HasValue)
                claims.Add(new Claim("Id_RegistroAcceso", idRegistro.Value.ToString()));

            var identity  = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccesoDenegado()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CambiarContrasena(bool modal = false)
        {
            ViewBag.Modal = modal;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CambiarContrasena(
            string ContrasenaActual, string ContrasenaNueva, string ConfirmarContrasena, bool modal = false)
        {
            if (ContrasenaNueva != ConfirmarContrasena)
            {
                ViewBag.Modal = modal;
                ViewBag.Error = "La nueva contraseña y la confirmación no coinciden.";
                return View();
            }

            var idClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(idClaim, out Guid id))
                return Unauthorized();

            var resultado = await _usuarioService.CambiarContrasena(id, new()
            {
                ContrasenaActual = ContrasenaActual,
                ContrasenaNueva  = ContrasenaNueva
            });

            if (!resultado)
            {
                ViewBag.Modal = modal;
                ViewBag.Error = "La contraseña actual es incorrecta.";
                return View();
            }

            if (modal)
            {
                return Content(@"<script>
                    window.parent.postMessage('crud-success', '*');
                </script>", "text/html");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Salir()
        {
            var idRegistroClaim = User.FindFirst("Id_RegistroAcceso")?.Value;
            if (Guid.TryParse(idRegistroClaim, out var idRegistro))
                await _registroAccesoService.CerrarSesion(idRegistro);

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }
    }
}
