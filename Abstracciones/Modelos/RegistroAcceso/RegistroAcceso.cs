namespace Abstracciones.Modelos.RegistroAcceso
{
    public class RegistroAcceso
    {
        public class RegistrarAccesoRequest
        {
            public Guid? Id_Usuario { get; set; }
            public string? Nombre_Usuario { get; set; }
            public bool Exitoso { get; set; }
        }

        public class RegistroAccesoResponse
        {
            public Guid Id_Registro { get; set; }
            public Guid? Id_Usuario { get; set; }
            public string? Nombre_Usuario { get; set; }
            public DateTime Fecha_Login { get; set; }
            public DateTime? Fecha_Logout { get; set; }
            public bool Exitoso { get; set; }

            public string Estado => !Exitoso ? "Fallido" : (Fecha_Logout == null ? "Activa" : "Cerrada");
        }
    }
}
