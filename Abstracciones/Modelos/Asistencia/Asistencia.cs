using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos.Asistencia
{
    public class Asistencia
    {
        public static readonly string[] TiposEventoValidos = ["Falta", "Retardo", "DiaTrabajado"];

        public class AsistenciaRequest
        {
            [Required(ErrorMessage = "El empleado es requerido")]
            public Guid Id_Trabajador { get; set; }

            [Required(ErrorMessage = "La fecha es requerida")]
            public DateOnly Fecha { get; set; }

            [Required(ErrorMessage = "El tipo de registro es requerido")]
            public string? Tipo_Evento { get; set; }

            [StringLength(200, ErrorMessage = "Las observaciones no pueden superar los 200 caracteres")]
            public string? Observaciones { get; set; }
        }

        public class AsistenciaResponse : AsistenciaRequest
        {
            public Guid Id_Asistencia { get; set; }
            public DateTime Fecha_Registro { get; set; }
            public string? Nombre_Trabajador { get; set; }
        }

        public class ResumenAsistenciaResponse
        {
            public int Faltas { get; set; }
            public int Retardos { get; set; }
            public int Dias_Trabajados { get; set; }
            public decimal? Salario_Diario { get; set; }
            public decimal? Descuento_Estimado { get; set; }
        }
    }
}
