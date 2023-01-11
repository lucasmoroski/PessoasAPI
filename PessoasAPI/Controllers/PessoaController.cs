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
                pessoas.Telefone = param["telefone"].ToString();

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
        
        [AllowAnonymous]
        [HttpPut("AtualiazarPessoa")]
        public ActionResult AtualizarPessoa([FromBody] JObject param)
        {
            try
            {
                Pessoa pessoas = new Pessoa();
                pessoas.PessoaId = int.Parse(param["pessoaId"].ToString());
                pessoas.Name = param["nome"].ToString();
                pessoas.Email = param["email"].ToString();
                pessoas.Idade = int.Parse(param["idade"].ToString());
                pessoas.Telefone = param["telefone"].ToString();

                _pessoaRepositorio.Update(pessoas);

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

        [AllowAnonymous]
        [HttpDelete("DeletarPessoa")]
        public ActionResult DeletarPessoa([FromQuery] int pessoaId)
        {
            try
            {
                Pessoa pessoas = new Pessoa();
                pessoas.PessoaId = int.Parse(pessoaId.ToString());

                _pessoaRepositorio.Delet(pessoas.PessoaId);

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
    }
}
