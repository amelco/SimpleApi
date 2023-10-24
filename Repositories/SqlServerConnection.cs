using Microsoft.Data.SqlClient;
using SimpleApi.Interfaces;

namespace SimpleApi.Repositories
{
    public class SqlServerConnection : IDatabaseConnection
    {
        private SqlConnection? _conexao;
        private SqlCommand? _comando;
        private SqlDataReader? _reader;

        public SqlServerConnection(SqlCommand sqlCommand)
        {
            Abre(sqlCommand);
        }

        public T? Query<T>(Func<SqlDataReader, T> funcao)
        {
            if (_comando is null)
                return default(T);

            _reader = _comando.ExecuteReader();
            if (_reader is null)
                return default(T);

            if (!_reader.HasRows)
                return default(T);

            var retorno = funcao(_reader);

            Fecha();

            return retorno;
        }

        public bool NonQuery()
        {
            if (_comando is null)
                return false;

            var linhasAfetadas = _comando.ExecuteNonQuery();
            if (linhasAfetadas <= 0)
                return false;

            return true;
        }

        // TODO: substituir sqlCommand string por sqlCommand command. Dessa forma, passamos parametros e evitamos sqlCommand injections
        // ==> private void Abre(SqlCommand sqlCommand, string[] parametros, object[] valores)
        //                                              \----------------||-----------------/
        //                                                               \/
        //                                          Construir uma classe para isso (SqlParametros)
        //private void Abre(string sqlCommand)
        private void Abre(SqlCommand cmd)
        {
            try
            {
                var cs = "Data Source=testeteste.database.windows.net;Initial Catalog=Teste;Encrypt=True;Connection Timeout=30;Authentication=Active Directory Default;";
                _conexao = new SqlConnection(cs);
                if (_conexao == null)
                {
                    return;
                }
                _conexao.Open();
                //Console.WriteLine("Abriu conexao!");
                //var cmd = new SqlCommand(sqlCommand, _conexao);
                //if (cmd == null)
                //{
                //    return;
                //}
                //cmd.Parameters.Clear();
                _comando = cmd;
            }
            catch
            {
                throw;
            }

        }

        private void Fecha()
        {
            if (_reader != null)
                _reader.Close();
            if (_conexao != null)
                _conexao.Close();
            //Console.WriteLine("Fechou conexao!");
        }

    }
}
