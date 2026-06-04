using Microsoft.AspNetCore.Mvc;
using Web.Services;
using static Abstracciones.Modelos.Producto.Producto;

namespace Web.Controllers
{
    public class ProductoController : Controller
    {
        private readonly ProductoService _productoService;

        public ProductoController(ProductoService productoService)
        {
            _productoService = productoService;
        }

        public async Task<IActionResult> Index()
        {
            var productos = await _productoService.Obtener();
            return View(productos);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(ProductoRequest producto)
        {
            var resultado = await _productoService.Agregar(producto);

            if (!resultado)
                return View(producto);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Editar(Guid id)
        {
            var producto = await _productoService.Obtener(id);

            if (producto == null)
                return NotFound();

            var modelo = new ProductoRequest
            {
                Id_Producto = producto.Id_Producto,
                Nombre_Producto = producto.Nombre_Producto,
                Precio_Venta = producto.Precio_Venta,
                Stock_Actual = producto.Stock_Actual
            };

            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Guid id, ProductoRequest producto)
        {
            var resultado = await _productoService.Editar(id, producto);

            if (!resultado)
                return View(producto);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Eliminar(Guid id)
        {
            await _productoService.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }
    }
}