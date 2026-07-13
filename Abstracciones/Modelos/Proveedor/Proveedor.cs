using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos.Proveedor
{
    public class Proveedor
    {
        public class ProveedorBase
        {
            [Required(ErrorMessage = "La identificación es requerida")]
            [StringLength(20, MinimumLength = 9, ErrorMessage = "La identificación debe tener entre 9 y 20 caracteres")]
            [RegularExpression(@"^\d+$", ErrorMessage = "La identificación solo debe contener números")]
            public string? Identificacion_Proveedor { get; set; }

            [Required(ErrorMessage = "El nombre del proveedor es requerido")]
            [StringLength(150, MinimumLength = 2, ErrorMessage = "Debe tener entre 2 y 150 caracteres")]
            public string? Nombre_Proveedor { get; set; }

            [Required(ErrorMessage = "El teléfono es requerido")]
            [Phone(ErrorMessage = "El formato del teléfono no es válido")]
            [StringLength(20, ErrorMessage = "El teléfono no puede superar los 20 caracteres")]
            public string? Telefono_Proveedor { get; set; }

            [Required(ErrorMessage = "El correo es requerido")]
            [EmailAddress(ErrorMessage = "El formato del correo no es válido")]
            [StringLength(200, ErrorMessage = "El correo no puede superar los 200 caracteres")]
            public string? Correo_Proveedor { get; set; }
        }

        public class ProveedorRequest : ProveedorBase
        {
            public Guid Id_Proveedor { get; set; }
        }

        public class ProveedorResponse : ProveedorBase
        {
            public Guid Id_Proveedor { get; set; }
            public int Id_Estado { get; set; }
        }
    }
}
