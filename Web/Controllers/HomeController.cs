using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web.Models;

namespace Web.Controllers;

[Authorize]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        var nombre   = User.Identity?.Name ?? "PT";
        var initials = string.Concat(nombre.Split(' ')
            .Take(2)
            .Select(p => p[0]))
            .ToUpper();

        var rol = User.FindFirstValue(ClaimTypes.Role) ?? "";

        var todosModulos = new List<DashboardModuleViewModel>
        {
            new()
            {
                Key = "usuarios",
                Roles = ["Admin"],
                Name = "Usuarios",
                Tag = "Acceso y seguridad",
                Description = "Gestión de cuentas del sistema para acceso operativo del personal.",
                Accent = "#C41E1E",
                Table = new()
                {
                    Title = "Directorio de usuarios",
                    Columns = ["Usuario", "Trabajador", "Estado"],
                    EmptyMessage = "Sin usuarios registrados.",
                    SourceUrl = "https://localhost:44378/api/Usuario"
                }
            },
            new()
            {
                Key = "planilla",
                Roles = ["Admin"],
                Name = "Planilla",
                Tag = "Recursos humanos",
                Description = "Control de trabajadores para operación diaria y administración del personal.",
                Accent = "#0B3D6E",
                Table = new()
                {
                    Title = "Empleados",
                    Columns = ["Cédula", "Nombre", "Puesto", "Estado"],
                    EmptyMessage = "Sin trabajadores registrados.",
                    SourceUrl = "https://localhost:44378/api/Trabajador"
                }
            },
            new()
            {
                Key = "puestos",
                Roles = ["Admin"],
                Name = "Puestos",
                Tag = "Organización",
                Description = "Catálogo de puestos para estructurar funciones y responsabilidades del equipo.",
                Accent = "#4A6C2F",
                Table = new()
                {
                    Title = "Catálogo de puestos",
                    Columns = ["Puesto", "Estado"],
                    EmptyMessage = "Sin puestos registrados.",
                    SourceUrl = "https://localhost:44378/api/Puesto"
                }
            },
            new()
            {
                Key = "productos",
                Roles = ["Admin", "Cajas"],
                Name = "Productos",
                Tag = "Catálogo",
                Description = "Inventario de productos con control de precio y stock.",
                Accent = "#D4920A",
                Table = new()
                {
                    Title = "Catálogo de productos",
                    Columns = ["Nombre", "Proveedor", "Precio", "Stock", "Estado"],
                    EmptyMessage = "Sin productos registrados.",
                    SourceUrl = "https://localhost:44378/api/Producto"
                }
            },
            new()
            {
                Key = "inventario",
                Roles = ["Admin", "Panadero"],
                Name = "Inventario",
                Tag = "Almacén",
                Description = "Control de ingredientes y materiales utilizados en la producción.",
                Accent = "#4A6C2F",
                Table = new()
                {
                    Title = "Items de inventario",
                    Columns = ["Nombre", "Unidad", "Stock actual", "Stock mínimo", "Proveedor", "Estado"],
                    EmptyMessage = "Sin items registrados.",
                    SourceUrl = "https://localhost:44378/api/Inventario"
                }
            },
            new()
            {
                Key = "proveedores",
                Roles = ["Admin"],
                Name = "Proveedores",
                Tag = "Abastecimiento",
                Description = "Directorio de proveedores para abastecimiento y compras.",
                Accent = "#0B3D6E",
                Table = new()
                {
                    Title = "Directorio de proveedores",
                    Columns = ["Nombre", "Teléfono", "Correo", "Estado"],
                    EmptyMessage = "Sin proveedores registrados.",
                    SourceUrl = "https://localhost:44378/api/Proveedor"
                }
            }
        };

        var modulosDelRol = todosModulos
            .Where(m => m.Roles.Contains(rol))
            .ToList();

        var model = new DashboardViewModel
        {
            Today        = DateTime.Now,
            UserInitials = initials,
            Modules      = modulosDelRol
        };

        return View(model);
    }
}
