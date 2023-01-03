using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PessoasAPI.Models;
using PessoasAPI.Repositorio;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PessoasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        private readonly PessoaRepositorio _pessoaRepositorio;
        public PessoaController() 
        { 
            _pessoaRepositorio= new PessoaRepositorio();    
        }

        [HttpGet("GetListPessoas")]
        public ActionResult<List<Pessoa>> GetListPessoas()
        {
            List<Pessoa> pessoas = new List<Pessoa>();
            pessoas = _pessoaRepositorio.BuscarPessoas();
            return pessoas;
        }
    }
}
