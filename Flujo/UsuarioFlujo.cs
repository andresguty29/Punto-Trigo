using Abstracciones.Interfaces.DA.UsuarioDA;
using Abstracciones.Interfaces.Flujo.Usuario;
using static Abstracciones.Modelos.Usuario.Usuario;

namespace Flujo
{
    public class UsuarioFlujo : IUsuarioFlujo
    {
        private IUsuarioDA _usuarioDA;

        public UsuarioFlujo(IUsuarioDA usuarioDA)
        {
            _usuarioDA = usuarioDA;
        }

        public Task<Guid> Agregar(UsuarioRequest usuario)
        {
            return _usuarioDA.Agregar(usuario);
        }

        public Task<Guid> Editar(Guid Id, UsuarioRequest usuario)
        {
            return _usuarioDA.Editar(Id, usuario);
        }

        public Task<Guid> Eliminar(Guid Id)
        {
            return _usuarioDA.Eliminar(Id);
        }

        public Task<IEnumerable<UsuarioResponse>> Obtener()
        {
            return _usuarioDA.Obtener();
        }

        public Task<UsuarioResponse> Obtener(Guid Id)
        {
            return _usuarioDA.Obtener(Id);
        }

        public Task<Guid> Activar(Guid Id)
        {
            return _usuarioDA.Activar(Id);
        }
    }
}
