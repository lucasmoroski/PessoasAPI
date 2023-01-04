using Microsoft.Data.SqlClient;
using PessoasAPI.Models;
using PessoasAPI.Repositorio.interfaces;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace PessoasAPI.Repositorio
{
    public class ContatoRepositorio : IContatosRepositorio
    {
        string connection = @"Data Source=DESKTOP-036I1NH\SQLEXPRESS;Initial Catalog=Pessoa_db;Integrated Security=True";

        public List<Contato> BuscarContatos(int pessoaId)
        {
            List<Contato> contatos = new List<Contato>();

            using (SqlConnection conection = new SqlConnection(connection))
            {
                conection.Open();
                using (SqlCommand cmd = new SqlCommand("sp_ListaContatos", conection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("pessoaId", pessoaId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader != null) 
                        {
                            while (reader.Read())
                            {
                                var c = new Contato();
                                c.ContatoId = int.Parse(reader["ContatoId"].ToString());
                                c.PessoaId = int.Parse(reader["fk_pessoaId"].ToString());
                                c.Name = reader["Nome"].ToString();
                                c.Email = reader["Email"].ToString();
                                c.Idade = int.Parse(reader["Idade"].ToString());
                                c.Phone = long.Parse(reader["Telefone"].ToString());
                                contatos.Add(c);
                            }
                        }
                    }
                }
            }
            return contatos;
        }

        public Contato Create(Contato pessoa)
        {
            Contato pessoas = new Contato();

            using (SqlConnection conection = new SqlConnection(connection))
            {
                conection.Open();
                using (SqlCommand cmd = new SqlCommand("sp_InsertPessoaId", conection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("nome", pessoa.Name.ToString());
                    cmd.Parameters.AddWithValue("email", pessoa.Email.ToString());
                    cmd.Parameters.AddWithValue("idade", int.Parse(pessoa.Idade.ToString()));
                    cmd.Parameters.AddWithValue("phone", long.Parse(pessoa.Phone.ToString()));
                    cmd.Parameters.AddWithValue("number", long.Parse(pessoa.Number.ToString()));

                    pessoas.PessoaId = cmd.ExecuteNonQuery();

                }

            }
            return pessoas;
        }


        public Task<bool> Delet(int pessoaId, int contatoId)
        {
            throw new System.NotImplementedException();
        }

        public Task<Contato> Update(Contato contato)
        {
            throw new System.NotImplementedException();
        }

    }
}
