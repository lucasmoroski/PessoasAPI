using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using PessoasAPI.Models;
using PessoasAPI.Repositorio;
using System;
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
            _pessoaRepositorio = new PessoaRepositorio();
        }

        //{"nome": "teste", "email": "teste@gmail.com", "idade": "23", "phone": "54454654654", "number": "0"}

        [AllowAnonymous]
        [HttpPost("CadastrarPessoa")]
        public ActionResult CadastrarPessoa([FromBody] JObject param)
        {
            try
            {
                Pessoa pessoas = new Pessoa();
                pessoas.Name = param["nome"].ToString();
                pessoas.Email = param["email"].ToString();
                pessoas.Idade = int.Parse(param["idade"].ToString());
                pessoas.Phone = long.Parse(param["phone"].ToString());
                pessoas.Number = long.Parse(param["number"].ToString());

                _pessoaRepositorio.Create(pessoas);

                var ret = new
                {
                    status = true,
                    msg = "sucesso",
                    pessoas.PessoaId
                };
                return Ok(ret);
            }
            catch (Exception e)
            {
                var retorno = new
                {
                    status = false,
                    msg = e.Message
                };
                return BadRequest(retorno);
            }
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
