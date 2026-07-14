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
        var idTrabajador = User.FindFirstValue("Id_Trabajador") ?? "";

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
                    Columns = ["Imagen", "Nombre", "Proveedor", "Precio", "Stock", "Estado"],
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
                Key = "produccion",
                Roles = ["Admin"],
                Name = "Produccion",
                Tag = "Plan diario",
                Description = "Lista diaria de tareas por empleado con productos asignados y cantidades de produccion.",
                Accent = "#8A6A2B",
                Table = new()
                {
                    Title = "Lista diaria de produccion",
                    Columns = ["Empleado", "Producto", "Cantidad diaria", "Realizado", "Estado"],
                    EmptyMessage = "Sin tareas diarias registradas.",
                    SourceUrl = "https://localhost:44378/api/Produccion/lista-diaria"
                }
            },
            new()
            {
                Key = "mi_produccion",
                Roles = ["Panadero"],
                Name = "Mi Produccion",
                Tag = "Plan diario",
                Description = "Tu lista de produccion asignada para hoy, con materiales y receta para imprimir.",
                Accent = "#8A6A2B",
                Table = new()
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
                    Columns = ["Identificación", "Nombre", "Teléfono", "Correo", "Estado"],
                    EmptyMessage = "Sin proveedores registrados.",
                    SourceUrl = "https://localhost:44378/api/Proveedor"
                }
            },
            new()
            {
                Key = "compras",
                Roles = ["Admin"],
                Name = "Compras",
                Tag = "Gastos y proveedores",
                Description = "Registro de facturas de proveedores, control de egresos y stock de insumos.",
                Accent = "#8A6A2B",
                Table = new()
            },
            new()
            {
                Key = "tiquetes",
                Roles = ["Admin", "Cajas"],
                Name = "Tiquetes",
                Tag = "Ventas",
                Description = "Historial de tiquetes electronicos emitidos (Hacienda simulado).",
                Accent = "#1D5EA8",
                Table = new()
            },
            new()
            {
                Key = "reportes",
                Roles = ["Admin"],
                Name = "Reportes",
                Tag = "Rentabilidad",
                Description = "Comparativo de ingresos y egresos con graficas, exportacion a Excel/PDF.",
                Accent = "#4A6C2F",
                Table = new()
            },
            new()
            {
                Key = "clientes",
                Roles = ["Admin"],
                Name = "Clientes",
                Tag = "Facturación",
                Description = "Directorio de clientes frecuentes para agilizar la emisión de comprobantes.",
                Accent = "#4A6C2F",
                Table = new()
                {
                    Title = "Directorio de clientes",
                    Columns = ["Cédula", "Nombre", "Correo", "Teléfono", "Estado"],
                    EmptyMessage = "Sin clientes registrados.",
                    SourceUrl = "https://localhost:44378/api/Cliente"
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
            CurrentRole  = rol,
            IdTrabajador = idTrabajador,
            Modules      = modulosDelRol
        };

        return View(model);
    }
}
