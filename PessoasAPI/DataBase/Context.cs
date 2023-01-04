using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PessoasAPI.Models;
using System.IO;

namespace PessoasAPI.DataBase
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> dados) : base(dados)
        {

        }

        public DbSet<Pessoa> Pessoas { get; set; }

        public DbSet<Contato> Contatos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           IConfiguration config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", false , true).Build();

            optionsBuilder.UseSqlServer(config.GetConnectionString("ConnectionDBA"));
        }

    }
}
