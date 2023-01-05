using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using PessoasAPI.Models;
using PessoasAPI.Repositorio;
using System;
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
            List<Contato> contato = new List<Contato>();
            contato = _contatoRepositorio.BuscarContatos(pessoaId);
            return contato;
        }

        [AllowAnonymous]
        [HttpPost("CadastrarContato")]
        public ActionResult CadastrarContato([FromBody] JObject param)
        {
            try
            {
                Contato contato = new Contato();
                contato.PessoaId = int.Parse(param["pessoaId"].ToString());
                contato.Name = param["nome"].ToString();
                contato.Email = param["email"].ToString();
                contato.Idade = int.Parse(param["idade"].ToString());
                contato.Telefone = param["telefone"].ToString();

                _contatoRepositorio.Create(contato);

                var ret = new
                {
                    status = true,
                    msg = "sucesso"
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
        [HttpPut("AtualiazarContato")]
        public ActionResult AtualizarContato([FromBody] JObject param)
        {
            try
            {
                Contato contato = new Contato();
                contato.PessoaId = int.Parse(param["pessoaId"].ToString());
                contato.ContatoId = int.Parse(param["contatoId"].ToString());
                contato.Name = param["nome"].ToString();
                contato.Email = param["email"].ToString();
                contato.Idade = int.Parse(param["idade"].ToString());
                contato.Telefone = param["telefone"].ToString();

                _contatoRepositorio.Update(contato);

                var ret = new
                {
                    status = true,
                    msg = "sucesso",
                    contato.ContatoId
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
        [HttpDelete("DeletarContato")]
        public ActionResult DeletarContato([FromQuery] int pessoaId, [FromQuery] int contatoId)
        {
            try
            {
                Contato contato = new Contato();
                contato.PessoaId = int.Parse(pessoaId.ToString());
                contato.ContatoId = int.Parse(contatoId.ToString());

                _contatoRepositorio.Delet(contato.PessoaId,contato.ContatoId);

                var ret = new
                {
                    status = true,
                    msg = "sucesso",
                    contato.ContatoId
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
