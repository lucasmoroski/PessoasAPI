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

        public readonly Context _context;

        public PessoaRepositorio()
        {
            _context = new Context();
        }

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
                                p.Telefone = reader["Telefone"].ToString();
                                pessoa.Add(p);
                            }
                        }
                    }
                }
            }
            return pessoa;
        }

        public long Create(Pessoa pessoa)
        {
            Pessoa pessoas = new Pessoa();
            SqlCommand cmd = _context.ExecuteStoredProcedure("sp_InsertPessoaId");

            cmd.Parameters.AddWithValue("@nome", pessoa.Name.ToString());
            cmd.Parameters.AddWithValue("@email", pessoa.Email.ToString());
            cmd.Parameters.AddWithValue("@idade", int.Parse(pessoa.Idade.ToString()));
            cmd.Parameters.AddWithValue("@telefone", pessoa.Telefone.ToString());

            cmd.ExecuteNonQuery();

            return 1;

        }

        public bool Delet(int pessoaId)
        {
            using (SqlConnection conection = new SqlConnection(connection))
            {
                conection.Open();

                using (SqlCommand cmd = new SqlCommand("sp_DeletPessoaId", conection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@pessoaId", pessoaId);

                    cmd.ExecuteNonQuery();

                    return true;
                }

            }
        }


        public long Update(Pessoa pessoa)
        {

            using (SqlConnection conection = new SqlConnection(connection))
            {
                conection.Open();

                using (SqlCommand cmd = new SqlCommand("sp_UpdatePessoaId", conection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@pessoaId", int.Parse(pessoa.PessoaId.ToString()));
                    cmd.Parameters.AddWithValue("@nome", pessoa.Name.ToString());
                    cmd.Parameters.AddWithValue("@email", pessoa.Email.ToString());
                    cmd.Parameters.AddWithValue("@idade", int.Parse(pessoa.Idade.ToString()));
                    cmd.Parameters.AddWithValue("@telefone", pessoa.Telefone.ToString());

                    cmd.ExecuteNonQuery();

                    return 1;
                }

            }
        }
    }
}
