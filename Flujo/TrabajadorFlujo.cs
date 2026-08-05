using Abstracciones.Interfaces.DA.TrabajadorDA;
using Abstracciones.Interfaces.Flujo.Trabajador;
using Microsoft.Data.SqlClient;
using static Abstracciones.Modelos.Trabajador.Trabajador;

namespace Flujo
{
    public class TrabajadorFlujo : ITrabajadorFlujo
    {
        private ITrabajadorDA _trabajadorDA;

        public TrabajadorFlujo(ITrabajadorDA trabajadorDA)
        {
            _trabajadorDA = trabajadorDA;
        }

        public async Task<Guid> Agregar(TrabajadorRequest trabajador)
        {
            try
            {
                return await _trabajadorDA.Agregar(trabajador);
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                throw new InvalidOperationException("Ya existe un trabajador con esa cédula.");
            }
        }

        public async Task<Guid> Editar(Guid Id, TrabajadorRequest trabajador)
        {
            try
            {
                return await _trabajadorDA.Editar(Id, trabajador);
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                throw new InvalidOperationException("Ya existe un trabajador con esa cédula.");
            }
        }

        public Task<Guid> Eliminar(Guid Id)
        {
            return _trabajadorDA.Eliminar(Id);
        }

        public Task<IEnumerable<TrabajadorResponse>> Obtener()
        {
            return _trabajadorDA.Obtener();
        }

        public Task<TrabajadorResponse> Obtener(Guid Id)
        {
            return _trabajadorDA.Obtener(Id);
        }

        public Task<Guid> Activar(Guid Id)
        {
            return _trabajadorDA.Activar(Id);
        }

        public Task<IEnumerable<TrabajadorResponse>> ObtenerPanaderos()
        {
            return _trabajadorDA.ObtenerPanaderos();
        }

        public async Task<Guid> ConfigurarPago(Guid Id, ConfigurarPagoRequest configuracion)
        {
            if (!TiposPagoValidos.Contains(configuracion.Tipo_Pago))
                throw new InvalidOperationException("El tipo de pago indicado no es válido.");

            try
            {
                return await _trabajadorDA.ConfigurarPago(Id, configuracion);
            }
            catch (SqlException ex) when (ex.Number == 50000)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<CalculoPagoResponse> CalcularPago(Guid Id, decimal? horasTrabajadas)
        {
            try
            {
                var resultado = await _trabajadorDA.CalcularPago(Id, horasTrabajadas);

                if (resultado == null)
                    throw new InvalidOperationException("No fue posible calcular el pago con la información disponible.");

                return resultado;
            }
            catch (SqlException ex) when (ex.Number == 50000)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
