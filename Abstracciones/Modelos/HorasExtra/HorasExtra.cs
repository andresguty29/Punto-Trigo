using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos.HorasExtra
{
    public class HorasExtra
    {
        public class HorasExtraRequest
        {
            [Required(ErrorMessage = "El empleado es requerido")]
            public Guid Id_Trabajador { get; set; }

            [Required(ErrorMessage = "La fecha es requerida")]
            public DateOnly Fecha { get; set; }

            [Range(0.01, 24, ErrorMessage = "La cantidad de horas debe ser mayor a 0")]
            public decimal Horas { get; set; }
        }

        public class HorasExtraResponse : HorasExtraRequest
        {
            public Guid Id_HorasExtra { get; set; }
            public decimal? Tarifa_Aplicada { get; set; }
            public decimal? Monto_Calculado { get; set; }
            public DateTime Fecha_Registro { get; set; }
            public string? Nombre_Trabajador { get; set; }
        }
    }
}
