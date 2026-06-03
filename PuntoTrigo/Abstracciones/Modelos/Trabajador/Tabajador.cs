using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos.Trabajador
{
    public class Trabajador
    {
        public class TrabajadorBase
        {
            public string? Cedula { get; set; }
            public string? Nombre_Completo { get; set; }
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
        }
    }
}
