﻿using Microsoft.Data.SqlClient;
using SimpleApi.Entities;

namespace SimpleApi.Repositories
{
    public class ClienteRepository 
    {
        public int ObtemNumeroDeClientes()
        {
            var sql = "select count(*) from Clientes";
            var retorno = ExecutadorSQL.Query(sql, MapeiaContaClientes);
            return retorno;
        }

        public List<Cliente>? ListarClientes()
        {
            var sql = "select * from Clientes";
            var retorno = ExecutadorSQL.Query(sql, MapeiaListarClientes);
            return retorno;
        }

        public bool SalvarCliente(Cliente cliente)
        {
            // TODO: passar dados apropriadamente via PARAMETERS poara evitar sql injection
            var sql = $"insert into Clientes (Nome, Email, Usuario, Senha) VALUES ('{cliente.Nome}', '{cliente.Email}', '{cliente.Usuario}', {cliente.Senha})";
            var sucesso = ExecutadorSQL.NonQuery(sql);
            return sucesso;
        }

        private int MapeiaContaClientes(SqlDataReader reader)
        {
            if (reader.Read())
            {
                return reader.GetInt32(0);
            }
            return 0;
        }

        private List<Cliente> MapeiaListarClientes(SqlDataReader reader)
        {
            List<Cliente> clientes = new();
            while (reader.Read())
            {
                clientes.Add(new Cliente
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    Email = reader.GetString(2),
                    Usuario = reader.GetString(3),
                    Senha = reader.GetInt32(4)
                });
            }
            return clientes;

        }
    }
}
