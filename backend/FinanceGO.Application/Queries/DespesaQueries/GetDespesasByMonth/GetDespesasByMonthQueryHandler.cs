using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FinanceGO.Application.ViewModels;
using FinanceGO.Core.Repositories.DespesaRepositories;
using FinanceGO.Core.UserServices;
using MediatR;

namespace FinanceGO.Application.Queries.DespesaQueries.GetDespesasByMonth
{
    public class GetDespesasByMonthQueryHandler : IRequestHandler<GetDespesasByMonthQuery, List<DespesaViewModel>>
    {
        private readonly IDespesaQueryRepository _queryRepository;
        private readonly IMapper _mapper;
        private readonly int _loggedUserId;

        public GetDespesasByMonthQueryHandler(IDespesaQueryRepository queryRepository, IMapper mapper, ILoggedUserService usuarioService)
        {
            _queryRepository = queryRepository;
            _mapper = mapper;
            _loggedUserId = usuarioService.GetUserId();
        }

        public async Task<List<DespesaViewModel>> Handle(GetDespesasByMonthQuery request, CancellationToken cancellationToken)
        {
            var despesasDoMes = await _queryRepository.GetDespesasByMonthAndUserAsync(request.Mes, request.Ano, _loggedUserId);

            return _mapper.Map<List<DespesaViewModel>>(despesasDoMes);
        }
    }
}