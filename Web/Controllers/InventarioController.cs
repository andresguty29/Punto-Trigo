using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Services;
using static Abstracciones.Modelos.Inventario.Inventario;
using static Abstracciones.Modelos.Inventario.Movimiento;

namespace Web.Controllers
{
    public class InventarioController : Controller
    {
        private readonly InventarioService _inventarioService;
        private readonly ProveedorService _proveedorService;

        public InventarioController(InventarioService inventarioService, ProveedorService proveedorService)
        {
            _inventarioService = inventarioService;
            _proveedorService = proveedorService;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _inventarioService.Obtener();
            return View(items);
        }

        [HttpGet]
        public async Task<IActionResult> Crear(bool modal = false)
        {
            await CargarProveedores();
            ViewBag.Modal = modal;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(InventarioRequest inventario, bool modal = false)
        {
            var (ok, error) = await _inventarioService.Agregar(inventario);

            if (!ok)
            {
                await CargarProveedores();
                ViewBag.Modal = modal;
                ViewBag.ErrorApi = error;
                return View(inventario);
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
            var item = await _inventarioService.Obtener(id);

            if (item == null)
                return NotFound();

            var modelo = new InventarioRequest
            {
                Id_Inventario = item.Id_Inventario,
                Nombre        = item.Nombre,
                Unidad        = item.Unidad,
                Stock_Minimo  = item.Stock_Minimo,
                Id_Proveedor  = item.Id_Proveedor
            };

            await CargarProveedores();
            ViewBag.Modal = modal;
            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Guid id, InventarioRequest inventario, bool modal = false)
        {
            var (ok, error) = await _inventarioService.Editar(id, inventario);

            if (!ok)
            {
                await CargarProveedores();
                ViewBag.Modal = modal;
                ViewBag.ErrorApi = error;
                return View(inventario);
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
            await _inventarioService.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Activar(Guid id)
        {
            await _inventarioService.Activar(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Movimiento(Guid id, bool modal = false)
        {
            var item = await _inventarioService.Obtener(id);
            if (item == null) return NotFound();

            await CargarProveedores();
            ViewBag.Modal = modal;
            ViewBag.NombreItem = item.Nombre;

            return View(new MovimientoRequest { Id_Inventario = id });
        }

        [HttpPost]
        public async Task<IActionResult> Movimiento(Guid id, MovimientoRequest movimiento, bool modal = false)
        {
            try
            {
                await _inventarioService.RegistrarMovimiento(movimiento);

                if (modal)
                {
                    return Content(@"
                    <script>
                        window.parent.postMessage('crud-success', '*');
                    </script>", "text/html");
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                await CargarProveedores();
                ViewBag.Modal = modal;

                var item = await _inventarioService.Obtener(id);
                ViewBag.NombreItem = item?.Nombre;
                ViewBag.ErrorMovimiento = ex.Message.Contains("Stock insuficiente")
                    ? "Stock insuficiente para realizar la salida."
                    : "No se pudo registrar el movimiento. Intenta de nuevo.";

                return View(movimiento);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Historial(Guid id, bool modal = false)
        {
            var item = await _inventarioService.Obtener(id);
            if (item == null) return NotFound();

            var movimientos = await _inventarioService.ObtenerMovimientos(id);

            ViewBag.Modal = modal;
            ViewBag.NombreItem = item.Nombre;

            return View(movimientos);
        }

        private async Task CargarProveedores()
        {
            var proveedores = await _proveedorService.Obtener();

            ViewBag.Proveedores = proveedores
                .Where(p => p.Id_Estado == 1)
                .Select(p => new SelectListItem
                {
                    Value = p.Id_Proveedor.ToString(),
                    Text  = p.Nombre_Proveedor
                }).ToList();
        }
    }
}
