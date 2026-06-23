using Microsoft.AspNetCore.Mvc;
using Web.Services;
using static Abstracciones.Modelos.Proveedor.Proveedor;

namespace Web.Controllers
{
    public class ProveedorController : Controller
    {
        private readonly ProveedorService _proveedorService;

        public ProveedorController(ProveedorService proveedorService)
        {
            _proveedorService = proveedorService;
        }

        public async Task<IActionResult> Index()
        {
            var proveedores = await _proveedorService.Obtener();
            return View(proveedores);
        }

        [HttpGet]
        public IActionResult Crear(bool modal = false)
        {
            ViewBag.Modal = modal;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(ProveedorRequest proveedor, bool modal = false)
        {
            var (ok, error) = await _proveedorService.Agregar(proveedor);

            if (!ok)
            {
                ViewBag.Modal = modal;
                ViewBag.ErrorApi = error;
                return View(proveedor);
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
            var proveedor = await _proveedorService.Obtener(id);

            if (proveedor == null)
                return NotFound();

            var modelo = new ProveedorRequest
            {
                Id_Proveedor = proveedor.Id_Proveedor,
                Nombre_Proveedor = proveedor.Nombre_Proveedor,
                Telefono_Proveedor = proveedor.Telefono_Proveedor,
                Correo_Proveedor = proveedor.Correo_Proveedor
            };

            ViewBag.Modal = modal;
            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Guid id, ProveedorRequest proveedor, bool modal = false)
        {
            var (ok, error) = await _proveedorService.Editar(id, proveedor);

            if (!ok)
            {
                ViewBag.Modal = modal;
                ViewBag.ErrorApi = error;
                return View(proveedor);
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
            await _proveedorService.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Activar(Guid id)
        {
            await _proveedorService.Activar(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
