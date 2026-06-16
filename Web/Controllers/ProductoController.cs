using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Services;
using static Abstracciones.Modelos.Producto.Producto;

namespace Web.Controllers
{
    public class ProductoController : Controller
    {
        private readonly ProductoService _productoService;
        private readonly ProveedorService _proveedorService;

        public ProductoController(ProductoService productoService, ProveedorService proveedorService)
        {
            _productoService = productoService;
            _proveedorService = proveedorService;
        }

        public async Task<IActionResult> Index()
        {
            var productos = await _productoService.Obtener();
            return View(productos);
        }

        [HttpGet]
        public async Task<IActionResult> Crear(bool modal = false)
        {
            await CargarProveedores();
            ViewBag.Modal = modal;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(ProductoRequest producto, bool modal = false)
        {
            var resultado = await _productoService.Agregar(producto);

            if (!resultado)
            {
                await CargarProveedores();
                ViewBag.Modal = modal;
                return View(producto);
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
            var producto = await _productoService.Obtener(id);

            if (producto == null)
                return NotFound();

            var modelo = new ProductoRequest
            {
                Id_Producto = producto.Id_Producto,
                Id_Proveedor = producto.Id_Proveedor,
                Nombre_Producto = producto.Nombre_Producto,
                Precio_Venta = producto.Precio_Venta,
                Stock_Actual = producto.Stock_Actual
            };

            await CargarProveedores();
            ViewBag.Modal = modal;
            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Guid id, ProductoRequest producto, bool modal = false)
        {
            var resultado = await _productoService.Editar(id, producto);

            if (!resultado)
            {
                await CargarProveedores();
                ViewBag.Modal = modal;
                return View(producto);
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
            await _productoService.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Activar(Guid id)
        {
            await _productoService.Activar(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task CargarProveedores()
        {
            var proveedores = await _proveedorService.Obtener();

            ViewBag.Proveedores = proveedores.Select(p => new SelectListItem
            {
                Value = p.Id_Proveedor.ToString(),
                Text = p.Nombre_Proveedor
            }).ToList();
        }
    }
}
