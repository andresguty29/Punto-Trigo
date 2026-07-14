using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos.Cliente
{
    public class Cliente
    {
        public class ClienteBase
        {
            [Required(ErrorMessage = "La cédula es requerida")]
            [StringLength(20, MinimumLength = 9, ErrorMessage = "La cédula debe tener entre 9 y 20 caracteres")]
            [RegularExpression(@"^\d+$", ErrorMessage = "La cédula solo debe contener números")]
            public string? Cedula { get; set; }

            [Required(ErrorMessage = "El nombre completo es requerido")]
            [StringLength(150, MinimumLength = 3, ErrorMessage = "Debe tener entre 3 y 150 caracteres")]
            public string? Nombre_Completo { get; set; }

            [EmailAddress(ErrorMessage = "El formato del correo no es válido")]
            [StringLength(200, ErrorMessage = "El correo no puede superar los 200 caracteres")]
            public string? Correo_Cliente { get; set; }

            [Phone(ErrorMessage = "El formato del teléfono no es válido")]
            [StringLength(20, ErrorMessage = "El teléfono no puede superar los 20 caracteres")]
            public string? Telefono_Cliente { get; set; }
        }

        public class ClienteRequest : ClienteBase
        {
            public Guid Id_Cliente { get; set; }
        }

        public class ClienteResponse : ClienteBase
        {
            public Guid Id_Cliente { get; set; }
            public int Id_Estado { get; set; }
        }
    }
}
