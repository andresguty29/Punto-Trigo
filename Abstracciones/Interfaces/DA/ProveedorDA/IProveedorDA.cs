using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Abstracciones.Modelos.Proveedor.Proveedor;

namespace Abstracciones.Interfaces.DA.ProveedorDA
{
    public interface IProveedorDA
    {
        Task<IEnumerable<ProveedorResponse>> Obtener();
        Task<ProveedorResponse> Obtener(Guid Id);
        Task<Guid> Agregar(ProveedorRequest proveedor);
        Task<Guid> Editar(Guid Id, ProveedorRequest proveedor);
        Task<Guid> Eliminar(Guid Id);
        Task<Guid> Activar(Guid Id);
    }
}
