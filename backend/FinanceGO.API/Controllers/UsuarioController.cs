using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceGO.Application.Commands.UsuarioCommands.CreateUsuario;
using FinanceGO.Application.Commands.UsuarioCommands.DeleteUsuario;
using FinanceGO.Application.Commands.UsuarioCommands.LoginUsuario;
using FinanceGO.Application.Commands.UsuarioCommands.UpdateSenha;
using FinanceGO.Application.Commands.UsuarioCommands.UpdateUsuario;
using FinanceGO.Application.Queries.UsuarioQueries.GetUsuarioById;
using FinanceGO.Core.AuthServices;
using FinanceGO.Core.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinanceGO.API.Controllers
{
    [ApiController]
    [Route("usuarios")]
    public class UsuarioController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsuarioController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuarioById(int id)
        {
            var query = new GetUsuarioByIdQuery(id);
            var usuario = await _mediator.Send(query);

            if(usuario == null) return NotFound();

            return Ok(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUsuario([FromBody] CreateUsuarioCommand command)
        {
            var usuario = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetUsuarioById), new { Id = usuario.Id}, usuario);
        }

        [HttpPost("/login")]
        public async Task<IActionResult> LoginUsuario([FromBody] LoginUsuarioCommand command)
        {
            var loginViewModel = await _mediator.Send(command);

            return Ok(loginViewModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, [FromBody] UpdateUsuarioCommand command)
        {
            var resultado = await _mediator.Send(command);

            if(resultado is RegistroNaoEncontradoResult) 
                return NotFound();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateSenha(int id, [FromBody] UpdateSenhaCommand command)
        {
            var resultado = await _mediator.Send(command);

            if(resultado is RegistroNaoEncontradoResult) return NotFound();
            else if(resultado is DadosInformadosNaoConferemResult) 
                return BadRequest("Os dados informados estão incorretos. Verifique");
            else if(resultado is RegistroAtualizadoComSucessoResult)
                return NoContent();
            else
                return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id, [FromBody] DeleteUsuarioCommand command)
        {
            var resultado = await _mediator.Send(command);

            if(resultado is RegistroNaoEncontradoResult) 
                return NotFound();
            else if(resultado is DadosInformadosNaoConferemResult)
                return BadRequest("Os dados informados estão incorretos. Verifique");
            else if(resultado is DeletadoComSucessoResult)
                return NoContent();
            else
                return BadRequest();
        }
/*
removerusuario
*/
    }
}