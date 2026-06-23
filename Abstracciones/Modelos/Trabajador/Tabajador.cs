using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos.Trabajador
{
    public class Trabajador
    {
        public class TrabajadorBase
        {
            [Required(ErrorMessage = "La cédula es requerida")]
            [StringLength(20, MinimumLength = 9, ErrorMessage = "La cédula debe tener entre 9 y 20 caracteres")]
            [RegularExpression(@"^\d+$", ErrorMessage = "La cédula solo debe contener números")]
            public string? Cedula { get; set; }

            [Required(ErrorMessage = "El nombre completo es requerido")]
            [StringLength(150, MinimumLength = 3, ErrorMessage = "Debe tener entre 3 y 150 caracteres")]
            public string? Nombre_Completo { get; set; }

            [Required(ErrorMessage = "El puesto es requerido")]
            public Guid Id_Puesto { get; set; }
        }

        public class TrabajadorRequest : TrabajadorBase
        {
            public Guid Id_Trabajador { get; set; }
        }

        public class TrabajadorResponse : TrabajadorBase
        {
            public Guid Id_Trabajador { get; set; }
            public int Id_Estado { get; set; }
            public string? Nombre_Puesto { get; set; }
        }
    }
}
