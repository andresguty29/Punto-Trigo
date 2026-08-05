using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos.Inventario
{
    public class Movimiento
    {
        public class MovimientoBase
        {
            [Required(ErrorMessage = "El item de inventario es requerido")]
            public Guid Id_Inventario { get; set; }

            [Required(ErrorMessage = "El tipo de movimiento es requerido")]
            public string? Tipo { get; set; }

            [Range(0.01, double.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0")]
            public decimal Cantidad { get; set; }

            [StringLength(200, ErrorMessage = "El motivo no puede superar los 200 caracteres")]
            public string? Motivo { get; set; }

            public Guid? Id_Proveedor { get; set; }

            public DateOnly? Fecha_Vencimiento { get; set; }

            [Range(0, 99999999.99, ErrorMessage = "El costo no puede ser negativo")]
            public decimal? Costo_Unitario { get; set; }
        }

        public class MovimientoRequest : MovimientoBase { }

        public class MovimientoResponse : MovimientoBase
        {
            public Guid Id_Movimiento { get; set; }
            public DateTime Fecha { get; set; }
            public string? Nombre_Proveedor { get; set; }
            public int Id_Estado { get; set; }
        }
    }
}
