using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceGO.Application.Commands.ReceitaCommands.CreateReceita;
using FinanceGO.Application.Commands.ReceitaCommands.DeleteReceita;
using FinanceGO.Application.Commands.ReceitaCommands.UpdateReceita;
using FinanceGO.Application.Queries.ReceitaQueries.GetReceitaById;
using FinanceGO.Application.Queries.ReceitaQueries.GetReceitas;
using FinanceGO.Application.Queries.ReceitaQueries.GetReceitasByMonth;
using FinanceGO.Application.ViewModels;
using FinanceGO.Core.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinanceGO.API.Controllers
{
    [Route("receitas")]
    [ApiController]
    public class ReceitaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReceitaController(IMediator mediator)
        {
            _mediator = mediator;
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
            var receita = await _mediator.Send(query);

            if(receita == null) return NotFound();

            return Ok(receita);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReceita([FromBody] CreateReceitaCommand command)
        {
            var resultado = await _mediator.Send(command);

            if(resultado is RegistroDuplicadoResult)
                return BadRequest("Já existe receita cadastrada com a mesma descrição para este mês");
            else if(resultado is CriadoComSucessoResult)
            {
                var receitaViewModel = (ReceitaViewModel) resultado.Value;
                return CreatedAtAction(nameof(GetReceitaById), new { Id = receitaViewModel.Id}, receitaViewModel);
            }
            else 
                return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReceita(int id, [FromBody] UpdateReceitaCommand command)
        {
            var resultado = await _mediator.Send(command);

            if(resultado is RegistroNaoEncontradoResult) 
                return NotFound();
            else if(resultado is RegistroDuplicadoResult)
                return BadRequest("Já existe receita cadastrada com a mesma descrição para este mês");
            else if(resultado is RegistroAtualizadoComSucesso) 
                return NoContent();
            else 
                return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReceita(int id)
        {
            var command = new DeleteReceitaCommand(id);
            var resultado = await _mediator.Send(command);

            if(resultado is RegistroNaoEncontradoResult) return NotFound();
            else if(resultado is DeletadoComSucessoResult) return NoContent();
            else return BadRequest();
        }
    }
}