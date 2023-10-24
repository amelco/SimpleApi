using Microsoft.Data.SqlClient;

namespace SimpleApi.Repositories
{
    public static class ExecutadorSQL
    {
        public static T? Query<T>(SqlCommand sqlCommand, Func<SqlDataReader, T> funcao)
        {
            var conexao = new SqlServerConnection(sqlCommand);
            T? resultado = conexao.Query(funcao);
            return resultado;
        }

        public static bool NonQuery(SqlCommand sqlCommand)
        {
            var conexao = new SqlServerConnection(sqlCommand);
            bool resultado = conexao.NonQuery();
            return resultado;
        }
    }
}
