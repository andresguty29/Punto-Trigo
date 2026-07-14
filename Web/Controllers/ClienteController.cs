using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Services;
using static Abstracciones.Modelos.Cliente.Cliente;

namespace Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ClienteController : Controller
    {
        private readonly ClienteService _clienteService;

        public ClienteController(ClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        public async Task<IActionResult> Index()
        {
            var clientes = await _clienteService.Obtener();
            return View(clientes);
        }

        [HttpGet]
        public IActionResult Crear(bool modal = false)
        {
            ViewBag.Modal = modal;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(ClienteRequest cliente, bool modal = false)
        {
            var (ok, error) = await _clienteService.Agregar(cliente);

            if (!ok)
            {
                ViewBag.Modal = modal;
                ViewBag.ErrorApi = string.IsNullOrWhiteSpace(error)
                    ? "No se pudo guardar el cliente."
                    : error;
                return View(cliente);
            }

            if (modal)
            {
                return Content(@"
            <script>
                window.parent.postMessage('crud-success', '*');
            </script>", "text/html");
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Editar(Guid id, bool modal = false)
        {
            var cliente = await _clienteService.Obtener(id);

            if (cliente == null)
                return NotFound();

            var modelo = new ClienteRequest
            {
                Id_Cliente = cliente.Id_Cliente,
                Cedula = cliente.Cedula,
                Nombre_Completo = cliente.Nombre_Completo,
                Correo_Cliente = cliente.Correo_Cliente,
                Telefono_Cliente = cliente.Telefono_Cliente
            };

            ViewBag.Modal = modal;
            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Guid id, ClienteRequest cliente, bool modal = false)
        {
            var (ok, error) = await _clienteService.Editar(id, cliente);

            if (!ok)
            {
                ViewBag.Modal = modal;
                ViewBag.ErrorApi = string.IsNullOrWhiteSpace(error)
                    ? "No se pudo actualizar el cliente."
                    : error;
                return View(cliente);
            }

            if (modal)
            {
                return Content(@"
            <script>
                window.parent.postMessage('crud-success', '*');
            </script>", "text/html");
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Eliminar(Guid id)
        {
            await _clienteService.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Activar(Guid id)
        {
            await _clienteService.Activar(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
