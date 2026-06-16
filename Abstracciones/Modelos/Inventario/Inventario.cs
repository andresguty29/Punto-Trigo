namespace Abstracciones.Modelos.Inventario
{
    public class Inventario
    {
        public class InventarioBase
        {
            public string? Nombre { get; set; }
            public string? Unidad { get; set; }
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
