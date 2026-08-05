using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos.Prestamo
{
    public class Prestamo
    {
        public class PrestamoRequest
        {
            [Required(ErrorMessage = "El empleado es requerido")]
            public Guid Id_Trabajador { get; set; }

            [Range(0.01, 99999999.99, ErrorMessage = "El monto debe ser mayor a 0")]
            public decimal Monto { get; set; }

            [Required(ErrorMessage = "La fecha es requerida")]
            public DateOnly Fecha { get; set; }

            [StringLength(200, ErrorMessage = "La descripción no puede superar los 200 caracteres")]
            public string? Descripcion { get; set; }
        }

        public class PrestamoResponse : PrestamoRequest
        {
            public Guid Id_Prestamo { get; set; }
            public decimal Saldo_Pendiente { get; set; }
            public DateTime Fecha_Registro { get; set; }
            public string? Nombre_Trabajador { get; set; }
        }
    }
}
