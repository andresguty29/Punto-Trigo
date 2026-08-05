namespace Abstracciones.Modelos.Perdida
{
    public class Perdida
    {
        public class VencimientoPendienteResponse
        {
            public Guid Id_Movimiento { get; set; }
            public Guid Id_Inventario { get; set; }
            public string? Nombre_Inventario { get; set; }
            public string? Unidad { get; set; }
            public decimal Cantidad { get; set; }
            public DateOnly Fecha_Vencimiento { get; set; }
            public decimal? Costo_Unitario { get; set; }
            public decimal Stock_Actual { get; set; }
        }

        public class ProcesarPerdidaResponse
        {
            public Guid Id_Perdida { get; set; }
            public decimal Costo_Total { get; set; }
        }
    }
}
