using Dapper;
using System.Data;

namespace DA.Repositorios
{
    public class DateOnlyTypeHandler : SqlMapper.TypeHandler<DateOnly>
    {
        public override void SetValue(IDbDataParameter parameter, DateOnly value)
        {
            parameter.DbType = DbType.Date;
            parameter.Value = value.ToDateTime(TimeOnly.MinValue);
        }

        public override DateOnly Parse(object value)
        {
            return DateOnly.FromDateTime((DateTime)value);
        }
    }

    public static class DapperTypeHandlers
    {
        private static bool _registrado;

        public static void Registrar()
        {
            if (_registrado) return;
            SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
            _registrado = true;
        }
    }
}
