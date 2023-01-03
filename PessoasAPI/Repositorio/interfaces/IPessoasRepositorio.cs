using PessoasAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PessoasAPI.Repositorio.interfaces
{
    public interface IPessoasRepositorio
    {
        List<Pessoa> BuscarPessoas();
        Pessoa Create(Pessoa pessoa);
        Task<Pessoa> Update(Pessoa pessoa);
        Task<bool> Delet(int pessoaId);
    }
}
