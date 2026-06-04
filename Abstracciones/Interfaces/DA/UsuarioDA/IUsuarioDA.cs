using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Abstracciones.Modelos.Usuario.Usuario;

namespace Abstracciones.Interfaces.DA.UsuarioDA
{
    public interface IUsuarioDA
    {
        Task<IEnumerable<UsuarioResponse>> Obtener();
        Task<UsuarioResponse> Obtener(Guid Id);
        Task<Guid> Agregar(UsuarioRequest usuario);
        Task<Guid> Editar(Guid Id, UsuarioRequest usuario);
        Task<Guid> Eliminar(Guid Id);
    }
}
