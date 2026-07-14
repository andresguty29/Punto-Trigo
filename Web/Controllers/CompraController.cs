using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Services;
using static Abstracciones.Modelos.Compra.Compra;

namespace Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CompraController : Controller
    {
        private readonly CompraService _compraService;
        private readonly ProveedorService _proveedorService;
        private readonly InventarioService _inventarioService;

        public CompraController(CompraService compraService, ProveedorService proveedorService, InventarioService inventarioService)
        {
            _compraService = compraService;
            _proveedorService = proveedorService;
            _inventarioService = inventarioService;
        }

        [HttpGet]
        public async Task<IActionResult> Historial(DateOnly? fechaInicio, DateOnly? fechaFin, decimal? montoMinimo)
        {
            var (compras, error) = await _compraService.Obtener(fechaInicio, fechaFin, montoMinimo);

            ViewBag.FechaInicio = fechaInicio;
            ViewBag.FechaFin = fechaFin;
            ViewBag.MontoMinimo = montoMinimo;
            ViewBag.ErrorApi = error;

            return View(compras);
        }

        [HttpGet]
        public async Task<IActionResult> Crear(bool modal = false)
        {
            await CargarCombos();
            ViewBag.Modal = modal;
            return View(new CompraRequest());
        }

        [HttpPost]
        public async Task<IActionResult> Crear(CompraRequest compra, bool modal = false)
        {
            var (ok, error) = await _compraService.Agregar(compra);

            if (!ok)
            {
                await CargarCombos();
                ViewBag.Modal = modal;
                ViewBag.ErrorApi = string.IsNullOrWhiteSpace(error)
                    ? "No se pudo registrar la compra."
                    : error;
                return View(compra);
            }

            if (modal)
            {
                return Content(@"
                <script>
                    window.parent.postMessage('crud-success', '*');
                </script>", "text/html");
            }

            return RedirectToAction(nameof(Historial));
        }

        [HttpPost]
        public async Task<IActionResult> Anular(Guid id)
        {
            await _compraService.Anular(id);
            return RedirectToAction(nameof(Historial));
        }

        [HttpGet]
        public async Task<IActionResult> Reclasificar(Guid id, bool modal = false)
        {
            var compra = await _compraService.Obtener(id);
            if (compra == null) return NotFound();

            ViewBag.Modal = modal;
            ViewBag.NumeroFactura = compra.Numero_Factura;
            return View(new ReclasificarCompraRequestConId
            {
                Id_Compra = id,
                Categoria = compra.Categoria,
                Descripcion_Adicional = compra.Descripcion_Adicional
            });
        }

        [HttpPost]
        public async Task<IActionResult> Reclasificar(Guid id, ReclasificarCompraRequest reclasificacion, bool modal = false)
        {
            var (ok, error) = await _compraService.Reclasificar(id, reclasificacion);

            if (!ok)
            {
                ViewBag.Modal = modal;
                ViewBag.ErrorApi = string.IsNullOrWhiteSpace(error)
                    ? "No se pudo reclasificar la compra."
                    : error;
                return View(new ReclasificarCompraRequestConId
                {
                    Id_Compra = id,
                    Categoria = reclasificacion.Categoria,
                    Descripcion_Adicional = reclasificacion.Descripcion_Adicional
                });
            }

            if (modal)
            {
                return Content(@"
                <script>
                    window.parent.postMessage('crud-success', '*');
                </script>", "text/html");
            }

            return RedirectToAction(nameof(Historial));
        }

        private async Task CargarCombos()
        {
            var proveedores = await _proveedorService.Obtener();
            var inventario = await _inventarioService.Obtener();

            ViewBag.Proveedores = proveedores
                .Where(p => p.Id_Estado == 1)
                .OrderBy(p => p.Nombre_Proveedor)
                .Select(p => new SelectListItem
                {
                    Value = p.Id_Proveedor.ToString(),
                    Text = p.Nombre_Proveedor
                }).ToList();

            ViewBag.Inventario = inventario
                .Where(i => i.Id_Estado == 1)
                .OrderBy(i => i.Nombre)
                .ToList();

            ViewBag.Categorias = CategoriasValidas;
        }
    }

    public class ReclasificarCompraRequestConId : ReclasificarCompraRequest
    {
        public Guid Id_Compra { get; set; }
    }
}
