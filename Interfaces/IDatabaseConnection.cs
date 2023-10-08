using Microsoft.Data.SqlClient;

namespace SimpleApi.Interfaces
{
    public interface IDatabaseConnection 
    {
        // TODO: SqlDataReader deve ser genérico
        public T? Query<T>(Func<SqlDataReader, T> funcao);
        public bool NonQuery();
    }
}
