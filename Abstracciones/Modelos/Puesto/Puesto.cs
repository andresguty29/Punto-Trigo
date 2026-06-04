namespace Abstracciones.Modelos.Puesto
{
    public class Puesto
    {
        public class PuestoBase
        {
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

