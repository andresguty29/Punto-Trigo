using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos.Proveedor
{
    public class Proveedor
    {
        public class ProveedorBase
        {
            public string? Nombre_Proveedor { get; set; }
            public string? Telefono_Proveedor { get; set; }
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
