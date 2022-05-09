using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceGO.Application.Queries.ResumoQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinanceGO.API.Controllers
{   
    [Route("resumo")]
    [ApiController]
    public class ResumoController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ResumoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{mes}/{ano}")]
        public async Task<IActionResult> GetResumoByMonth(int mes, int ano)
        {
            var query = new GetResumoByMonthQuery(mes, ano);
            var resumo = await _mediator.Send(query);

            if(resumo == null) 
                return NotFound("Não há despesas nem receitas cadastradas para este mês");

            return Ok(resumo);
        }
    }
}