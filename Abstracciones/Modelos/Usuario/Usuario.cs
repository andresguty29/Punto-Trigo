using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos.Usuario
{
    public class Usuario
    {
        public class UsuarioBase
        {
            [Required(ErrorMessage = "El nombre de usuario es requerido")]
            [StringLength(100, MinimumLength = 3, ErrorMessage = "Debe tener entre 3 y 100 caracteres")]
            public string? Nombre_Usuario { get; set; }

            [StringLength(500)]
            public string? Contrasena { get; set; }

            [Required(ErrorMessage = "El trabajador es requerido")]
            public Guid Id_Trabajador { get; set; }

            [Required(ErrorMessage = "El rol es requerido")]
            public string? Rol { get; set; }
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

        public class LoginRequest
        {
            [Required(ErrorMessage = "El nombre de usuario es requerido")]
            public string? Nombre_Usuario { get; set; }

            [Required(ErrorMessage = "La contraseña es requerida")]
            public string? Contrasena { get; set; }
        }

        public class LoginResponse
        {
            public Guid Id_Usuario { get; set; }
            public string? Nombre_Usuario { get; set; }
            public Guid Id_Trabajador { get; set; }
            public string? Nombre_Trabajador { get; set; }
            public string? Rol { get; set; }
        }

        public class CambiarContrasenaRequest
        {
            [Required(ErrorMessage = "La contraseña actual es requerida")]
            public string? ContrasenaActual { get; set; }

            [Required(ErrorMessage = "La nueva contraseña es requerida")]
            [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
            public string? ContrasenaNueva { get; set; }
        }
    }
}
