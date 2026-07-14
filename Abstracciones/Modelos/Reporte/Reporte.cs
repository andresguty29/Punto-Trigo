namespace Abstracciones.Modelos.Reporte
{
    public class Reporte
    {
        public class ReporteDiaResponse
        {
            public DateTime Fecha { get; set; }
            public decimal Ingresos { get; set; }
            public decimal Egresos { get; set; }
            public decimal Utilidad => Ingresos - Egresos;
        }
    }
}
