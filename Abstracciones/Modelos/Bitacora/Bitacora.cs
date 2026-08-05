namespace Abstracciones.Modelos.Bitacora
{
    public class Bitacora
    {
        public class RegistrarBitacoraRequest
        {
            public Guid? Id_Usuario { get; set; }
            public string? Nombre_Usuario { get; set; }
            public string? Accion { get; set; }
            public string? Detalle { get; set; }
        }

        public class BitacoraResponse
        {
            public Guid Id_Bitacora { get; set; }
            public Guid? Id_Usuario { get; set; }
            public string? Nombre_Usuario { get; set; }
            public string? Accion { get; set; }
            public string? Detalle { get; set; }
            public DateTime Fecha_Hora { get; set; }
        }
    }
}
