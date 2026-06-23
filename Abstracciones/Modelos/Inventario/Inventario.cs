using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos.Inventario
{
    public class Inventario
    {
        public class InventarioBase
        {
            [Required(ErrorMessage = "El nombre es requerido")]
            [StringLength(100, MinimumLength = 2, ErrorMessage = "Debe tener entre 2 y 100 caracteres")]
            public string? Nombre { get; set; }

            [Required(ErrorMessage = "La unidad de medida es requerida")]
            public string? Unidad { get; set; }

            [Range(0, double.MaxValue, ErrorMessage = "El stock mínimo no puede ser negativo")]
            public decimal Stock_Minimo { get; set; }

            public Guid? Id_Proveedor { get; set; }
        }

        public class InventarioRequest : InventarioBase
        {
            public Guid Id_Inventario { get; set; }
        }

        public class InventarioResponse : InventarioBase
        {
            public Guid Id_Inventario { get; set; }
            public decimal Stock_Actual { get; set; }
            public int Id_Estado { get; set; }
            public string? Nombre_Proveedor { get; set; }
        }
    }
}
