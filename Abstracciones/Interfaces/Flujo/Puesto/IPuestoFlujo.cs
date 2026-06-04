using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Abstracciones.Modelos.Puesto.Puesto;

namespace Abstracciones.Interfaces.Flujo.Puesto
{
    public interface IPuestoFlujo
    {
        Task<IEnumerable<PuestoResponse>> Obtener();
        Task<PuestoResponse> Obtener(Guid Id);
        Task<Guid> Agregar(PuestoRequest puesto);
        Task<Guid> Editar(Guid Id, PuestoRequest puesto);
        Task<Guid> Eliminar(Guid Id);
    }
}
