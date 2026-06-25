using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos.Produccion
{
    public class Produccion
    {
        public class AsignacionBase
        {
            [Required(ErrorMessage = "El trabajador es requerido")]
            public Guid Id_Trabajador { get; set; }

            [Required(ErrorMessage = "El producto es requerido")]
            public Guid Id_Producto { get; set; }

            [Range(0.01, 9999999.99, ErrorMessage = "La cantidad diaria debe ser mayor a 0")]
            public decimal Cantidad_Diaria { get; set; }
        }

        public class AsignacionRequest : AsignacionBase
        {
            public Guid Id_Asignacion { get; set; }
        }

        public class AsignacionResponse : AsignacionBase
        {
            public Guid Id_Asignacion { get; set; }
            public int Id_Estado { get; set; }
            public string? Nombre_Trabajador { get; set; }
            public string? Nombre_Producto { get; set; }
            public DateTime Fecha_Asignacion { get; set; }
        }

        public class ListaProduccionResponse
        {
            public Guid Id_Asignacion { get; set; }
            public Guid Id_Trabajador { get; set; }
            public string? Nombre_Trabajador { get; set; }
            public Guid Id_Producto { get; set; }
            public string? Nombre_Producto { get; set; }
            public decimal Cantidad_Diaria { get; set; }
            public DateTime Fecha_Lista { get; set; }
        }

        public class ProductoAsignadoResponse
        {
            public Guid Id_Producto { get; set; }
            public string? Nombre_Producto { get; set; }
        }
    }
}
