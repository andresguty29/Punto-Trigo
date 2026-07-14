using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos.Tiquete
{
    public class Tiquete
    {
        public const string Emitido = "Emitido";
        public const string PendienteEnvio = "PendienteEnvio";
        public const string Anulado = "Anulado";

        public class DetalleTiqueteRequest
        {
            [Required(ErrorMessage = "El producto es requerido")]
            public Guid Id_Producto { get; set; }

            [Range(1, 9999, ErrorMessage = "La cantidad debe ser mayor a 0")]
            public int Cantidad { get; set; }

            [Range(0.01, 9999999.99, ErrorMessage = "El precio debe ser mayor a 0")]
            public decimal Precio_Unitario { get; set; }
        }

        public class DetalleTiqueteResponse : DetalleTiqueteRequest
        {
            public Guid Id_DetalleTiquete { get; set; }
            public Guid Id_Tiquete { get; set; }
            public string? Nombre_Producto { get; set; }
            public decimal Subtotal { get; set; }
        }

        public class TiqueteRequest
        {
            public Guid? Id_Cliente { get; set; }

            [Required(ErrorMessage = "Debe incluir al menos un producto")]
            [MinLength(1, ErrorMessage = "Debe incluir al menos un producto")]
            public List<DetalleTiqueteRequest> Detalles { get; set; } = [];

            // Solo para pruebas: fuerza que la simulacion de envio a Hacienda falle
            public bool SimularFallo { get; set; } = false;
        }

        public class TiqueteResponse
        {
            public Guid Id_Tiquete { get; set; }
            public string? Consecutivo { get; set; }
            public string? Clave { get; set; }
            public Guid? Id_Cliente { get; set; }
            public string? Nombre_Cliente { get; set; }
            public Guid? Id_Trabajador { get; set; }
            public string? Nombre_Trabajador { get; set; }
            public DateTime Fecha_Emision { get; set; }
            public string? Estado { get; set; }
            public decimal Monto_Total { get; set; }
        }
    }
}
