using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ER01.Conexoes
{
    public class SqlServer
    {
        private readonly SqlConnection _conexao;
        public SqlServer()
        {
            string stringConexao = File.ReadAllText("C:\\Conexoes\\string_conexao.txt");
            _conexao = new SqlConnection(stringConexao);
        }

        public void InserirPessoa(Entidades.Pessoa pessoa)
        {
            try
            {
                _conexao.Open();

                string query = @"INSERT INTO Pessoa
                                (
                                Nome,
                                Telefone
                                )
                                VALUES
                                (
                                @Nome,
                                @Telefone
                                );";

                using(var cmd = new SqlCommand(query, _conexao))
                {
                    cmd.Parameters.AddWithValue("@Nome", pessoa.Nome);
                    cmd.Parameters.AddWithValue("@Telefone", pessoa.Telefone);

                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                _conexao.Close();
            }
        }
    }
}
