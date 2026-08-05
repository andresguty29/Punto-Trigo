using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.RegularExpressions;
using Web.Services;
using static Abstracciones.Modelos.Producto.Producto;

namespace Web.Controllers
{
    [Authorize]
    public class ProductoController : Controller
    {
        private readonly ProductoService _productoService;
        private readonly ProveedorService _proveedorService;
        private readonly IWebHostEnvironment _env;

        public ProductoController(ProductoService productoService, ProveedorService proveedorService, IWebHostEnvironment env)
        {
            _productoService = productoService;
            _proveedorService = proveedorService;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var productos = await _productoService.Obtener();
            return View(productos);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Crear(bool modal = false)
        {
            await CargarProveedores();
            ViewBag.Modal = modal;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Crear(ProductoRequest producto, string? imagenBase64, bool modal = false)
        {
            producto.Id_Producto = Guid.NewGuid();

            if (!string.IsNullOrEmpty(imagenBase64))
            {
                var ruta = GuardarImagenBase64(producto.Id_Producto, imagenBase64);
                if (ruta != null) producto.Imagen_Path = ruta;
            }

            var (ok, error) = await _productoService.Agregar(producto);

            if (!ok)
            {
                await CargarProveedores();
                ViewBag.Modal = modal;
                ViewBag.ErrorApi = string.IsNullOrWhiteSpace(error)
                    ? "No se pudo guardar el producto."
                    : error;
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Editar(Guid id, bool modal = false)
        {
            var producto = await _productoService.Obtener(id);
            if (producto == null) return NotFound();

            var modelo = new ProductoRequest
            {
                Id_Producto    = producto.Id_Producto,
                Id_Proveedor   = producto.Id_Proveedor,
                Nombre_Producto = producto.Nombre_Producto,
                Precio_Venta   = producto.Precio_Venta,
                Stock_Actual   = producto.Stock_Actual,
                Imagen_Path    = producto.Imagen_Path,
                Codigo         = producto.Codigo
            };

            ViewBag.ImagenActual = producto.Imagen_Path;
            await CargarProveedores();
            ViewBag.Modal = modal;
            return View(modelo);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Editar(Guid id, ProductoRequest producto, string? imagenBase64, bool modal = false)
        {
            if (!string.IsNullOrEmpty(imagenBase64))
            {
                var ruta = GuardarImagenBase64(id, imagenBase64);
                if (ruta != null) producto.Imagen_Path = ruta;
            }

            var (ok, error) = await _productoService.Editar(id, producto);

            if (!ok)
            {
                ViewBag.ImagenActual = producto.Imagen_Path;
                await CargarProveedores();
                ViewBag.Modal = modal;
                ViewBag.ErrorApi = string.IsNullOrWhiteSpace(error)
                    ? "No se pudo actualizar el producto."
                    : error;
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

            ViewBag.Proveedores = proveedores
                .Where(p => p.Id_Estado == 1)
                .Select(p => new SelectListItem
                {
                    Value = p.Id_Proveedor.ToString(),
                    Text  = p.Nombre_Proveedor
                }).ToList();
        }

        private string? GuardarImagenBase64(Guid idProducto, string imagenBase64)
        {
            try
            {
                // Formato: "data:image/png;base64,iVBOR..."
                var coincidencia = Regex.Match(imagenBase64, @"^data:image/(\w+);base64,(.+)$");
                if (!coincidencia.Success) return null;

                var formato = coincidencia.Groups[1].Value.ToLower();
                var extension = formato switch
                {
                    "jpeg" => ".jpg",
                    "jpg"  => ".jpg",
                    "png"  => ".png",
                    "webp" => ".webp",
                    _      => null
                };
                if (extension == null) return null;

                var datos = Convert.FromBase64String(coincidencia.Groups[2].Value);

                var carpeta = Path.Combine(_env.WebRootPath, "images", "productos");
                Directory.CreateDirectory(carpeta);

                // Eliminar imagen anterior del mismo producto (distinta extensión)
                foreach (var ext in new[] { ".jpg", ".jpeg", ".png", ".webp" })
                {
                    var anterior = Path.Combine(carpeta, $"{idProducto}{ext}");
                    if (System.IO.File.Exists(anterior)) System.IO.File.Delete(anterior);
                }

                var nombreArchivo = $"{idProducto}{extension}";
                System.IO.File.WriteAllBytes(Path.Combine(carpeta, nombreArchivo), datos);

                return $"/images/productos/{nombreArchivo}";
            }
            catch
            {
                return null;
            }
        }
    }
}
