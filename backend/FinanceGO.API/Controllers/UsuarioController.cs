using System.Threading.Tasks;
using FinanceGO.Application.Commands.UsuarioCommands.CreateUsuario;
using FinanceGO.Application.Commands.UsuarioCommands.DeleteUsuario;
using FinanceGO.Application.Commands.UsuarioCommands.LoginUsuario;
using FinanceGO.Application.Commands.UsuarioCommands.UpdateSenha;
using FinanceGO.Application.Commands.UsuarioCommands.UpdateUsuario;
using FinanceGO.Application.InputModels.UsuarioInputModels;
using FinanceGO.Application.Queries.UsuarioQueries.GetUsuarioById;
using FinanceGO.Application.ViewModels;
using FinanceGO.Core.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceGO.API.Controllers
{
    [ApiController]
    [Route("usuarios")]
    [Authorize(Policy = "HasUserId")]
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
        [AllowAnonymous]
        public async Task<IActionResult> CreateUsuario([FromBody] CreateUsuarioCommand command)
        {
            var resultado = await _mediator.Send(command);

            if(resultado is RegistroDuplicadoResult)
                return BadRequest("Já existe usuário cadastrado com o mesmo e-mail");
            else if(resultado is CriadoComSucessoResult)
            {
                var usuarioViewModel = (UsuarioViewModel) resultado.Value;
                return CreatedAtAction(nameof(GetUsuarioById), new { Id = usuarioViewModel.Id}, usuarioViewModel);
            }
            else
                return BadRequest();
        }

        [HttpPost("/login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginUsuario([FromBody] LoginUsuarioCommand command)
        {
            var loginViewModel = await _mediator.Send(command);

            return Ok(loginViewModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, [FromBody] UpdateUsuarioInputModel inputModel)
        {
            var command = new UpdateUsuarioCommand(id, inputModel);
            var resultado = await _mediator.Send(command);

            if(resultado is RegistroNaoEncontradoResult) 
                return NotFound();
            else if(resultado is RegistroDuplicadoResult)
                return BadRequest("Já existe usuário cadastrado com o mesmo e-mail");
            else if(resultado is RegistroAtualizadoComSucessoResult)
                return NoContent();
            else
                return BadRequest();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateSenha(int id, [FromBody] UpdateSenhaInputModel inputModel)
        {
            var command = new UpdateSenhaCommand(id, inputModel);
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
        public async Task<IActionResult> DeleteUsuario(int id, [FromBody] DeleteUsuarioInputModel inputModel)
        {
            var command = new DeleteUsuarioCommand(id, inputModel);
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
    }
}