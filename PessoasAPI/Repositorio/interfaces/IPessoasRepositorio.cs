using PessoasAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PessoasAPI.Repositorio.interfaces
{
    public interface IPessoasRepositorio
    {
        List<Pessoa> BuscarPessoas();
        long Create(Pessoa pessoa);
        long Update(Pessoa pessoa);
        bool Delet(int pessoaId);
    }
}
