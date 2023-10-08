using Microsoft.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;

namespace SimpleApi.Repositories
{
    public static class ExecutadorSQL
    {
        public static T? Query<T>(string sql, Func<SqlDataReader,T> funcao)
        {
            var conexao = new SqlServerConnection(sql);
            T? resultado = conexao.Query(funcao);
            return resultado;
        }

        public static bool NonQuery(string sql)
        {
            var conexao = new SqlServerConnection(sql);
            bool resultado = conexao.NonQuery();
            return resultado;
        }
    }
}
