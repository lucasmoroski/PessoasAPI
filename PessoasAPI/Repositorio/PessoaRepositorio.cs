using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PessoasAPI.DataBase;
using PessoasAPI.Models;
using PessoasAPI.Repositorio.interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PessoasAPI.Repositorio
{
    public class PessoaRepositorio : IPessoasRepositorio
    {
        string connection = @"Data Source=DESKTOP-036I1NH\SQLEXPRESS;Initial Catalog=Pessoa_db;Integrated Security=True";

        public List<Pessoa> BuscarPessoas()
        {
            List<Pessoa> pessoa = new List<Pessoa>();

            using (SqlConnection conection = new SqlConnection(connection))
            {
                conection.Open();
                using (SqlCommand cmd = new SqlCommand("sp_ListaPessoa", conection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                var p = new Pessoa();
                                p.PessoaId = int.Parse(reader["PessoaId"].ToString());
                                p.Name = reader["Nome"].ToString();
                                p.Email = reader["Email"].ToString();
                                p.Idade = int.Parse(reader["Idade"].ToString());
                                p.Phone = long.Parse(reader["Telefone"].ToString());
                                pessoa.Add(p);
                            }
                        }
                    }
                }
            }
            return pessoa;
        }

        public Pessoa Create(Pessoa pessoa)
        {
            Pessoa pessoas = new Pessoa();

            using (SqlConnection conection = new SqlConnection(connection))
            {
                conection.Open();
                using (SqlCommand cmd = new SqlCommand("sp_InsertPessoaId", conection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("nome", pessoa.Name.ToString());
                    cmd.Parameters.AddWithValue("email", pessoa.Name.ToString());
                    cmd.Parameters.AddWithValue("idade", int.Parse(pessoa.Name.ToString()));
                    cmd.Parameters.AddWithValue("phone", long.Parse(pessoa.Name.ToString()));
                    cmd.Parameters.AddWithValue("number", long.Parse(pessoa.Name.ToString()));

                    cmd.ExecuteNonQuery();

                }

            }
            return pessoas;
        }

        public async Task<bool> Delet(int pessoaId)
        {

            return true;
        }


        public async Task<Pessoa> Update(Pessoa pessoa)
        {

            return pessoa;
        }
    }
}
