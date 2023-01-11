using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using PessoasAPI.Models;
using System.Data;
using System.IO;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace PessoasAPI.DataBase
{
    public class Context : DbContext
    {
        public readonly IConfiguration _configuration;

        public Context()
        {
        }

        public Context(DbContextOptions<Context> dados, IConfiguration configuration) : base(dados)
        {
            _configuration = configuration;
        }

        public DbSet<Pessoa> Pessoas { get; set; }

        public DbSet<Contato> Contatos { get; set; }

        public SqlCommand ExecuteStoredProcedure(string sp_)
        {
            IConfiguration config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", false, true).Build();

            string connection = config.GetConnectionString("ConnectionDBA").ToString();
            //string connection = _configuration.GetConnectionString("ConnectionDBA").ToString();
            using (SqlConnection conection = new SqlConnection(connection))
            {
                conection.Open();

                using (SqlCommand cmd = new SqlCommand("sp_InsertPessoaId", conection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    return cmd;
                }
            }

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           IConfiguration config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", false , true).Build();

            optionsBuilder.UseSqlServer(config.GetConnectionString("ConnectionDBA"));
        }

    }
}
