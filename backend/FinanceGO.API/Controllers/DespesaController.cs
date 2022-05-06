using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceGO.Application.Commands.DespesaCommands.CreateDespesa;
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

        [HttpPost]
        public async Task<IActionResult> CreateDespesa(CreateDespesaCommand command)
        {
            var resultado = await _mediator.Send(command);

            if(resultado is RegistroDuplicadoResult)
                return BadRequest("Já existe despesa com a mesma descrição cadastrada neste mês");
            else if(resultado is CriadoComSucessoResult)
            {
                var despesaViewModel = resultado.Value as DespesaViewModel;
                return CreatedAtAction(nameof(ReadDespesaById), new { Id = despesaViewModel.Id}, despesaViewModel); 
            }
            else
                return BadRequest();
        }

        private object ReadDespesaById()
        {
            throw new NotImplementedException();
        }
    }
}