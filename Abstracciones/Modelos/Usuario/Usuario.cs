namespace Abstracciones.Modelos.Usuario
{
    public class Usuario
    {
        public class UsuarioBase
        {
            public string? Nombre_Usuario { get; set; }
            public string? Contrasena { get; set; }
            public Guid Id_Trabajador { get; set; }
        }

        public class UsuarioRequest : UsuarioBase
        {
            public Guid Id_Usuario { get; set; }
        }

        public class UsuarioResponse : UsuarioBase
        {
            public Guid Id_Usuario { get; set; }
            public int Id_Estado { get; set; }
            public string? Nombre_Trabajador { get; set; }
        }
    }
}
