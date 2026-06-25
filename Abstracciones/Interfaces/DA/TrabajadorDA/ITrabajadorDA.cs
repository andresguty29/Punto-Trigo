using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Abstracciones.Modelos.Trabajador.Trabajador;

namespace Abstracciones.Interfaces.DA.TrabajadorDA
{
    public interface ITrabajadorDA
    {
        Task<IEnumerable<TrabajadorResponse>> Obtener();
        Task<TrabajadorResponse> Obtener(Guid Id);
        Task<Guid> Agregar(TrabajadorRequest trabajador);
        Task<Guid> Editar(Guid Id, TrabajadorRequest trabajador);
        Task<Guid> Eliminar(Guid Id);
        Task<Guid> Activar(Guid Id);
        Task<IEnumerable<TrabajadorResponse>> ObtenerPanaderos();
    }
}
