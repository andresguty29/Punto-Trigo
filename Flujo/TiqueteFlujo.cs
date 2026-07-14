using Abstracciones.Interfaces.DA.TiqueteDA;
using Abstracciones.Interfaces.Flujo.Tiquete;
using Microsoft.Data.SqlClient;
using static Abstracciones.Modelos.Tiquete.Tiquete;

namespace Flujo
{
    public class TiqueteFlujo : ITiqueteFlujo
    {
        private readonly ITiqueteDA _tiqueteDA;

        public TiqueteFlujo(ITiqueteDA tiqueteDA)
        {
            _tiqueteDA = tiqueteDA;
        }

        public async Task<Guid> Agregar(TiqueteRequest tiquete, Guid? idTrabajador)
        {
            if (tiquete.Detalles == null || tiquete.Detalles.Count == 0)
                throw new InvalidOperationException("El tiquete debe incluir al menos un producto.");

            // Simulacion del envio a Hacienda: no hay certificado digital ni credenciales ATV todavia.
            // El documento (consecutivo + clave) se genera siempre; solo cambia si queda "Emitido" o "PendienteEnvio".
            var envioExitoso = !tiquete.SimularFallo;
            var estadoInicial = envioExitoso ? Emitido : PendienteEnvio;

            try
            {
                return await _tiqueteDA.Agregar(tiquete, idTrabajador, estadoInicial);
            }
            catch (SqlException ex) when (ex.Number == 50000)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<Guid> ReintentarEnvio(Guid Id)
        {
            // Simulacion: al reintentar, se asume que el envio ahora es exitoso.
            try
            {
                return await _tiqueteDA.ReintentarEnvio(Id, Emitido);
            }
            catch (SqlException ex) when (ex.Number == 50000)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public Task<IEnumerable<TiqueteResponse>> Obtener()
        {
            return _tiqueteDA.Obtener();
        }

        public Task<TiqueteResponse> Obtener(Guid Id)
        {
            return _tiqueteDA.Obtener(Id);
        }

        public Task<IEnumerable<DetalleTiqueteResponse>> ObtenerDetalle(Guid Id_Tiquete)
        {
            return _tiqueteDA.ObtenerDetalle(Id_Tiquete);
        }
    }
}
