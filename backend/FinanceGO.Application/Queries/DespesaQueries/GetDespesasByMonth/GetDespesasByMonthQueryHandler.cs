using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FinanceGO.Application.ViewModels;
using FinanceGO.Core.Repositories.DespesaRepositories;
using MediatR;

namespace FinanceGO.Application.Queries.DespesaQueries.GetDespesasByMonth
{
    public class GetDespesasByMonthQueryHandler : IRequestHandler<GetDespesasByMonthQuery, List<DespesaViewModel>>
    {
        private readonly IDespesaQueryRepository _queryRepository;
        private readonly IMapper _mapper;

        public GetDespesasByMonthQueryHandler(IDespesaQueryRepository queryRepository, IMapper mapper)
        {
            _queryRepository = queryRepository;
            _mapper = mapper;
        }

        public async Task<List<DespesaViewModel>> Handle(GetDespesasByMonthQuery request, CancellationToken cancellationToken)
        {
            var despesasDoMes = await _queryRepository.GetDespesasByMonthAsync(request.Mes, request.Ano);

            return _mapper.Map<List<DespesaViewModel>>(despesasDoMes);
        }
    }
}