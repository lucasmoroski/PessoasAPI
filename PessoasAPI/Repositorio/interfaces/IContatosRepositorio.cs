using PessoasAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PessoasAPI.Repositorio.interfaces
{
    public interface IContatosRepositorio
    {
        List<Contato> BuscarContatos(int pessoaId);
        long Create(Contato contato);
        long Update(Contato contato);
        bool Delet(int pessoaId, int contatoId);
    }
}
