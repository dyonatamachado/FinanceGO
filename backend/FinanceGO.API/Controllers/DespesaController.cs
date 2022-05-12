using System.Threading.Tasks;
using FinanceGO.Application.Commands.DespesaCommands.CreateDespesa;
using FinanceGO.Application.Commands.DespesaCommands.DeleteDespesa;
using FinanceGO.Application.Commands.DespesaCommands.UpdateDespesa;
using FinanceGO.Application.Queries.DespesaQueries.GetDespesaById;
using FinanceGO.Application.Queries.DespesaQueries.GetDespesas;
using FinanceGO.Application.Queries.DespesaQueries.GetDespesasByMonth;
using FinanceGO.Application.ViewModels;
using FinanceGO.Core.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinanceGO.API.Controllers
{
    [Route("despesas")]
    [ApiController]
    public class DespesaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DespesaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetDespesas()
        {
            var query = new GetDespesasQuery();
            var despesas = await _mediator.Send(query);

            if(despesas.Count == 0) return NoContent();

            return Ok(despesas);
        }

        [HttpGet("{mes}/{ano}")]
        public async Task<IActionResult> GetDespesasByMonth(int mes, int ano)
        {
            var query = new GetDespesasByMonthQuery(mes, ano);

            var despesas = await _mediator.Send(query);

            if(despesas.Count == 0) return NoContent();

            return Ok(despesas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDespesaById(int id)
        {
            var query = new GetDespesaByIdQuery(id);

            var despesa = await _mediator.Send(query);
            
            if(despesa == null)
                return NotFound();
                
            return Ok(despesa);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDespesa(CreateDespesaCommand command)
        {
            var resultado = await _mediator.Send(command);

            if(resultado is RegistroDuplicadoResult)
                return BadRequest("Já existe despesa com a mesma descrição cadastrada neste mês");
            else if(resultado is CriadoComSucessoResult)
            {
                var despesaViewModel = (DespesaViewModel)resultado.Value;
                return CreatedAtAction(nameof(GetDespesaById), new { Id = despesaViewModel.Id}, despesaViewModel); 
            }
            else
                return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDespesa(int id, [FromBody] UpdateDespesaCommand command)
        {
            var resultado = await _mediator.Send(command);

            if(resultado is RegistroNaoEncontradoResult) 
                return NotFound();
            else if(resultado is RegistroDuplicadoResult) 
                return BadRequest("Já existe despesa com a mesma descrição cadastrada neste mês");
            else if(resultado is RegistroAtualizadoComSucessoResult) 
                return NoContent();
            else 
                return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDespesa(int id)
        {
            var query = new DeleteDespesaCommand(id);

            var resultado = await _mediator.Send(query);

            if(resultado is RegistroNaoEncontradoResult)
                return NotFound();
            else if(resultado is DeletadoComSucessoResult)
                return NoContent();
            else
                return BadRequest();            
        }
    }
}