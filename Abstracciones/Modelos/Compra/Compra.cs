using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos.Compra
{
    public class Compra
    {
        public static readonly string[] CategoriasValidas =
            ["Materia Prima", "Limpieza", "Mantenimiento", "Otro"];

        public class DetalleCompraBase
        {
            [Required(ErrorMessage = "El insumo es requerido")]
            public Guid Id_Inventario { get; set; }

            [Range(0.01, 9999999.99, ErrorMessage = "La cantidad debe ser mayor a 0")]
            public decimal Cantidad { get; set; }

            [Required(ErrorMessage = "La unidad es requerida")]
            [StringLength(20)]
            public string? Unidad_Ingresada { get; set; }

            [Range(0, 9999999.99, ErrorMessage = "El costo no puede ser negativo")]
            public decimal? Costo_Unitario { get; set; }

            public DateOnly? Fecha_Vencimiento { get; set; }
        }

        public class DetalleCompraRequest : DetalleCompraBase { }

        public class DetalleCompraResponse : DetalleCompraBase
        {
            public Guid Id_DetalleCompra { get; set; }
            public Guid Id_Compra { get; set; }
            public string? Nombre_Inventario { get; set; }
            public string? Unidad { get; set; }
        }

        public class CompraBase
        {
            [Required(ErrorMessage = "El proveedor es requerido")]
            public Guid Id_Proveedor { get; set; }

            [Required(ErrorMessage = "El numero de factura es requerido")]
            [StringLength(50, MinimumLength = 1, ErrorMessage = "Debe tener entre 1 y 50 caracteres")]
            public string? Numero_Factura { get; set; }

            [Required(ErrorMessage = "La categoria es requerida")]
            public string? Categoria { get; set; }

            [StringLength(200, ErrorMessage = "La descripcion no puede superar los 200 caracteres")]
            public string? Descripcion_Adicional { get; set; }

            [Range(0.01, 99999999.99, ErrorMessage = "El monto debe ser mayor a 0")]
            public decimal Monto_Total { get; set; }
        }

        public class CompraRequest : CompraBase
        {
            public Guid Id_Compra { get; set; }
            public List<DetalleCompraRequest> Detalles { get; set; } = [];
        }

        public class CompraResponse : CompraBase
        {
            public Guid Id_Compra { get; set; }
            public int Id_Estado { get; set; }
            public string? Nombre_Proveedor { get; set; }
            public DateTime Fecha_Compra { get; set; }
        }

        public class ReclasificarCompraRequest
        {
            [Required(ErrorMessage = "La categoria es requerida")]
            public string? Categoria { get; set; }

            [StringLength(200, ErrorMessage = "La descripcion no puede superar los 200 caracteres")]
            public string? Descripcion_Adicional { get; set; }
        }
    }
}
