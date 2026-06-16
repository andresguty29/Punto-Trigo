namespace Abstracciones.Modelos.Inventario
{
    public class Movimiento
    {
        public class MovimientoBase
        {
            public Guid Id_Inventario { get; set; }
            public string? Tipo { get; set; }
            public decimal Cantidad { get; set; }
            public string? Motivo { get; set; }
            public Guid? Id_Proveedor { get; set; }
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
