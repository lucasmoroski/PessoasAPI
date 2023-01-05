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
                                c.Telefone = reader["Telefone"].ToString();
                                contatos.Add(c);
                            }
                        }
                    }
                }
            }
            return contatos;
        }

        public long Create(Contato contato)
        {
            Contato pessoas = new Contato();

            using (SqlConnection conection = new SqlConnection(connection))
            {
                conection.Open();
                using (SqlCommand cmd = new SqlCommand("sp_InsertContato", conection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@pessoaId", contato.PessoaId.ToString());
                    cmd.Parameters.AddWithValue("@nome", contato.Name.ToString());
                    cmd.Parameters.AddWithValue("@email", contato.Email.ToString());
                    cmd.Parameters.AddWithValue("@idade", int.Parse(contato.Idade.ToString()));
                    cmd.Parameters.AddWithValue("@telefone", contato.Telefone.ToString());

                    cmd.ExecuteNonQuery();

                    return 1;
                }

            }
        }


        public bool Delet(int pessoaId, int contatoId)
        {
            using (SqlConnection conection = new SqlConnection(connection))
            {
                conection.Open();

                using (SqlCommand cmd = new SqlCommand("sp_DeleteContato", conection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@pessoaId", pessoaId);
                    cmd.Parameters.AddWithValue("@contatoId", contatoId);

                    cmd.ExecuteNonQuery();

                    return true;
                }

            }
        }

        public long Update(Contato contato)
        {
            using (SqlConnection conection = new SqlConnection(connection))
            {
                conection.Open();

                using (SqlCommand cmd = new SqlCommand("sp_UpdateContato", conection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@contatoId", contato.ContatoId.ToString());
                    cmd.Parameters.AddWithValue("@pessoaId", contato.PessoaId.ToString());
                    cmd.Parameters.AddWithValue("@nome", contato.Name.ToString());
                    cmd.Parameters.AddWithValue("@email", contato.Email.ToString());
                    cmd.Parameters.AddWithValue("@idade", int.Parse(contato.Idade.ToString()));
                    cmd.Parameters.AddWithValue("@telefone", contato.Telefone.ToString());

                    cmd.ExecuteNonQuery();

                    return 1;
                }

            }
        }

    }
}
