using PessoasAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PessoasAPI.Repositorio.interfaces
{
    public interface IContatosRepositorio
    {
        List<Contato> BuscarContatos(int pessoaId);
        Contato Create(Contato contato);
        Task<Contato> Update(Contato contato);
        Task<bool> Delet(int pessoaId, int contatoId);
    }
}
