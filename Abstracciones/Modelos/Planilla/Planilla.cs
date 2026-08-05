using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos.Planilla
{
    public class Planilla
    {
        public class GenerarDetallePagoRequest
        {
            [Required(ErrorMessage = "El empleado es requerido")]
            public Guid Id_Trabajador { get; set; }

            [Required(ErrorMessage = "El periodo es requerido")]
            [StringLength(30, ErrorMessage = "El periodo no puede superar los 30 caracteres")]
            public string? Periodo { get; set; }

            [Required(ErrorMessage = "La fecha de inicio es requerida")]
            public DateOnly Fecha_Inicio { get; set; }

            [Required(ErrorMessage = "La fecha de fin es requerida")]
            public DateOnly Fecha_Fin { get; set; }
        }

        public class DetallePagoResponse
        {
            public Guid Id_Planilla { get; set; }
            public Guid Id_Trabajador { get; set; }
            public string? Nombre_Trabajador { get; set; }
            public string? Periodo { get; set; }
            public DateOnly Fecha_Inicio { get; set; }
            public DateOnly Fecha_Fin { get; set; }
            public decimal? Salario_Base_Aplicado { get; set; }
            public decimal Ingreso_Horas_Extra { get; set; }
            public decimal Deduccion_Asistencia { get; set; }
            public decimal Deduccion_Prestamos { get; set; }
            public decimal Deduccion_CCSS { get; set; }
            public decimal Total_Ingresos { get; set; }
            public decimal Total_Deducciones { get; set; }
            public decimal Monto_Neto { get; set; }
            public DateTime Fecha_Generacion { get; set; }
        }
    }
}
