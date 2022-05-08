using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FinanceGO.Application.ViewModels;
using FinanceGO.Core.Repositories.ReceitaRepositories;
using MediatR;

namespace FinanceGO.Application.Queries.ReceitaQueries.GetReceitasByMonth
{
    public class GetReceitasByMonthQueryHandler : IRequestHandler<GetReceitasByMonthQuery, List<ReceitaViewModel>>
    {
        private readonly IReceitaQueryRepository _queryRepository;
        private readonly IMapper _mapper;

        public GetReceitasByMonthQueryHandler(IReceitaQueryRepository queryRepository, IMapper mapper)
        {
            _queryRepository = queryRepository;
            _mapper = mapper;
        }

        public async Task<List<ReceitaViewModel>> Handle(GetReceitasByMonthQuery request, CancellationToken cancellationToken)
        {
            var receitasDoMes = await _queryRepository.GetReceitasByMonthAsync(request.Mes, request.Ano);

            return _mapper.Map<List<ReceitaViewModel>>(receitasDoMes);
        }
    }
}