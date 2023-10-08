using Microsoft.Data.SqlClient;
using SimpleApi.Interfaces;

namespace SimpleApi.Repositories
{
    public class SqlServerConnection : IDatabaseConnection
    {
        private SqlConnection? _conexao;
        private SqlCommand? _comando;
        private SqlDataReader? _reader;

        public SqlServerConnection(string sql)
        {
            Abre(sql);
        }

        public T? Query<T>(Func<SqlDataReader,T> funcao)
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

        private void Abre(string sql)
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
                var cmd = new SqlCommand(sql, _conexao);
                if (cmd == null)
                {
                    return;
                }
                cmd.Parameters.Clear();
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
