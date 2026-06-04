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

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(ProveedorRequest proveedor)
        {
            var resultado = await _proveedorService.Agregar(proveedor);

            if (!resultado)
                return View(proveedor);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Editar(Guid id)
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

            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Guid id, ProveedorRequest proveedor)
        {
            var resultado = await _proveedorService.Editar(id, proveedor);

            if (!resultado)
                return View(proveedor);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Eliminar(Guid id)
        {
            await _proveedorService.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }
    }
}