using Microsoft.AspNetCore.Mvc;
using PessoasAPI.Models;
using PessoasAPI.Repositorio;
using System.Collections.Generic;

namespace PessoasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatoController : Controller
    {
        private readonly PessoaRepositorio _pessoaRepositorio;
        private readonly ContatoRepositorio _contatoRepositorio;
        public ContatoController()
        {
            _pessoaRepositorio = new PessoaRepositorio();
            _contatoRepositorio = new ContatoRepositorio();
        }

        [HttpGet("GetListContatos")]
        public ActionResult<List<Contato>> GetListContatos([FromQuery] int pessoaId)
        {
            List<Contato> pessoas = new List<Contato>();
            pessoas = _contatoRepositorio.BuscarContatos(pessoaId);
            return pessoas;
        }
    }
}
