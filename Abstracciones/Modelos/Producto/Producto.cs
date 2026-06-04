using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos.Producto
{
    public class Producto
    {
        public class ProductoBase
        {
            public string? Nombre_Producto { get; set; }
            public decimal Precio_Venta { get; set; }
            public int Stock_Actual {  get; set; }

        }
        public class ProductoRequest : ProductoBase
        {
            public Guid Id_Producto { get; set; }
        }
        public class ProductoResponse : ProductoBase
        {
            public Guid Id_Producto { get; set; }
            public int Estado { get; set; }
        }
    }
}
