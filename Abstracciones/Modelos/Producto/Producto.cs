using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos.Producto
{
    public class Producto
    {
        public class ProductoBase
        {
            public Guid? Id_Proveedor { get; set; }

            [Required(ErrorMessage = "El nombre del producto es requerido")]
            [StringLength(150, MinimumLength = 2, ErrorMessage = "Debe tener entre 2 y 150 caracteres")]
            public string? Nombre_Producto { get; set; }

            [Range(0.01, 9999999.99, ErrorMessage = "El precio debe ser mayor a 0")]
            public decimal Precio_Venta { get; set; }

            [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo")]
            public int Stock_Actual { get; set; }

            [StringLength(30, ErrorMessage = "El código no puede superar los 30 caracteres")]
            public string? Codigo { get; set; }
        }

        public class ProductoRequest : ProductoBase
        {
            public Guid Id_Producto { get; set; }
            public string? Imagen_Path { get; set; }
        }

        public class ProductoResponse : ProductoBase
        {
            public Guid Id_Producto { get; set; }
            public int Id_Estado { get; set; }
            public string? Nombre_Proveedor { get; set; }
            public string? Imagen_Path { get; set; }
        }
    }
}
