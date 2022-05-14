using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FinanceGO.Application.ViewModels;
using FinanceGO.Core.Repositories.DespesaRepositories;
using FinanceGO.Core.Repositories.ReceitaRepositories;
using FinanceGO.Core.Results;
using FinanceGO.Core.UserServices;
using MediatR;

namespace FinanceGO.Application.Queries.ResumoQueries
{
    public class GetResumoByMonthQueryHandler : IRequestHandler<GetResumoByMonthQuery, ResumoViewModel>
    {
        private readonly IDespesaQueryRepository _despesaRepository;
        private readonly IReceitaQueryRepository _receitaRepository;
        private readonly int _loggedUserId;

        public GetResumoByMonthQueryHandler(IDespesaQueryRepository despesaRepository, IReceitaQueryRepository receitaRepository, ILoggedUserService usuarioService)
        {
            _despesaRepository = despesaRepository;
            _receitaRepository = receitaRepository;
            _loggedUserId = usuarioService.GetUserId();
        }

        public async Task<ResumoViewModel> Handle(GetResumoByMonthQuery request, CancellationToken cancellationToken)
        {
            var despesasDoMes = await _despesaRepository.GetDespesasByMonthAndUserAsync(request.Mes, request.Ano, _loggedUserId);
            var receitasDoMes = await _receitaRepository.GetReceitasByMonthAndUserAsync(request.Mes, request.Ano, _loggedUserId);

            if(despesasDoMes.Count == 0 && receitasDoMes.Count == 0) return null;
            
            return new ResumoViewModel(request.Mes, request.Ano, despesasDoMes, receitasDoMes);
        }
    }
}