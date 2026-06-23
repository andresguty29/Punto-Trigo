using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos.Puesto
{
    public class Puesto
    {
        public class PuestoBase
        {
            [Required(ErrorMessage = "El nombre del puesto es requerido")]
            [StringLength(100, MinimumLength = 2, ErrorMessage = "Debe tener entre 2 y 100 caracteres")]
            public string? Nombre_Puesto { get; set; }
        }

        public class PuestoRequest : PuestoBase
        {
            public Guid Id_Puesto { get; set; }
        }

        public class PuestoResponse : PuestoBase
        {
            public Guid Id_Puesto { get; set; }
            public int Id_Estado { get; set; }
        }
    }
}
