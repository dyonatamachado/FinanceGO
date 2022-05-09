using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FinanceGO.Application.ViewModels;
using FinanceGO.Core.Repositories.DespesaRepositories;
using FinanceGO.Core.Repositories.ReceitaRepositories;
using FinanceGO.Core.Results;
using MediatR;

namespace FinanceGO.Application.Queries.ResumoQueries
{
    public class GetResumoByMonthQueryHandler : IRequestHandler<GetResumoByMonthQuery, ResumoViewModel>
    {
        private readonly IDespesaQueryRepository _despesaRepository;
        private readonly IReceitaQueryRepository _receitaRepository;

        public GetResumoByMonthQueryHandler(IDespesaQueryRepository despesaRepository, IReceitaQueryRepository receitaRepository)
        {
            _despesaRepository = despesaRepository;
            _receitaRepository = receitaRepository;
        }

        public async Task<ResumoViewModel> Handle(GetResumoByMonthQuery request, CancellationToken cancellationToken)
        {
            var despesasDoMes = await _despesaRepository.GetDespesasByMonthAsync(request.Mes, request.Ano);
            var receitasDoMes = await _receitaRepository.GetReceitasByMonthAsync(request.Mes, request.Ano);

            if(despesasDoMes.Count == 0 && receitasDoMes.Count == 0) return null;
            
            return new ResumoViewModel(request.Mes, request.Ano, despesasDoMes, receitasDoMes);
        }
    }
}