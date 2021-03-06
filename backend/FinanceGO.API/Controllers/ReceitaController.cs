using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceGO.Application.Commands.ReceitaCommands.CreateReceita;
using FinanceGO.Application.Commands.ReceitaCommands.DeleteReceita;
using FinanceGO.Application.Commands.ReceitaCommands.UpdateReceita;
using FinanceGO.Application.InputModels.ReceitaInputModels;
using FinanceGO.Application.Queries.ReceitaQueries.GetReceitaById;
using FinanceGO.Application.Queries.ReceitaQueries.GetReceitas;
using FinanceGO.Application.Queries.ReceitaQueries.GetReceitasByMonth;
using FinanceGO.Application.ViewModels;
using FinanceGO.Core.Results;
using FinanceGO.Core.UserServices;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceGO.API.Controllers
{
    [Route("receitas")]
    [ApiController]
    [Authorize(Policy = "HasUserId")]
    public class ReceitaController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly int _loggedUserId;

        public ReceitaController(IMediator mediator, ILoggedUserService usuarioService)
        {
            _mediator = mediator;
            _loggedUserId = usuarioService.GetUserId();
        }

        [HttpGet]
        public async Task<IActionResult> GetReceitas()
        {
            var query = new GetReceitasQuery();
            var receitas = await _mediator.Send(query);

            if(receitas.Count == 0) return NoContent();

            return Ok(receitas);
        }

        [HttpGet("{mes}/{ano}")]
        public async Task<IActionResult> GetReceitasByMonth(int mes, int ano)
        {
            var query = new GetReceitasByMonthQuery(mes, ano);
            var receitas = await _mediator.Send(query);

            if(receitas.Count == 0) return NoContent();

            return Ok(receitas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReceitaById(int id)
        {
            var query = new GetReceitaByIdQuery(id);
            var resultado = await _mediator.Send(query);

            if(resultado is RegistroNaoEncontradoResult)
                return NotFound();
            else if(resultado is UsuarioNaoAutorizadoResult)
                return Forbid();
            else if(resultado is RegistroEncontradoResult)
            {
                var receita = (ReceitaViewModel) resultado.Value;
                return Ok(receita);
            }
            else
                return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> CreateReceita([FromBody] CreateReceitaInputModel inputModel)
        {
            var command = new CreateReceitaCommand(inputModel, _loggedUserId);
            var resultado = await _mediator.Send(command);

            if(resultado is RegistroDuplicadoResult)
                return BadRequest("J?? existe receita cadastrada com a mesma descri????o para este m??s");
            else if(resultado is CriadoComSucessoResult)
            {
                var receitaViewModel = (ReceitaViewModel) resultado.Value;
                return CreatedAtAction(nameof(GetReceitaById), new { Id = receitaViewModel.Id}, receitaViewModel);
            }
            else 
                return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReceita(int id, [FromBody] UpdateReceitaInputModel inputModel)
        {
            var command = new UpdateReceitaCommand(id, inputModel);
            var resultado = await _mediator.Send(command);

            if(resultado is RegistroNaoEncontradoResult) 
                return NotFound();
            else if(resultado is UsuarioNaoAutorizadoResult)
                return Forbid();
            else if(resultado is RegistroDuplicadoResult)
                return BadRequest("J?? existe receita cadastrada com a mesma descri????o para este m??s");
            else if(resultado is RegistroAtualizadoComSucessoResult) 
                return NoContent();
            else 
                return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReceita(int id)
        {
            var command = new DeleteReceitaCommand(id);
            var resultado = await _mediator.Send(command);

            if(resultado is RegistroNaoEncontradoResult) 
                return NotFound();
            else if(resultado is UsuarioNaoAutorizadoResult)
                return Forbid();
            else if(resultado is DeletadoComSucessoResult) 
                return NoContent();
            else 
                return BadRequest();
        }
    }
}