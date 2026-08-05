namespace Abstracciones.Modelos.Vacacion
{
    public class Vacacion
    {
        public class VacacionAsignadaResponse
        {
            public Guid Id_Vacacion { get; set; }
            public Guid Id_Trabajador { get; set; }
            public int Anio_Antiguedad { get; set; }
            public int Dias_Asignados { get; set; }
            public DateTime Fecha_Asignacion { get; set; }
        }
    }
}
