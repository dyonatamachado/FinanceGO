using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FinanceGO.Application.ViewModels;
using FinanceGO.Core.Repositories.DespesaRepositories;
using MediatR;

namespace FinanceGO.Application.Queries.DespesaQueries.GetDespesas
{
    public class GetDespesasQueryHandler : IRequestHandler<GetDespesasQuery, List<DespesaViewModel>>
    {
        private readonly IDespesaQueryRepository _queryRepository;
        private readonly IMapper _mapper;

        public GetDespesasQueryHandler(IDespesaQueryRepository queryRepository, IMapper mapper)
        {
            _queryRepository = queryRepository;
            _mapper = mapper;
        }

        public async Task<List<DespesaViewModel>> Handle(GetDespesasQuery request, CancellationToken cancellationToken)
        {
            var despesas = await _queryRepository.GetDespesasAsync();

            return _mapper.Map<List<DespesaViewModel>>(despesas);
        }
    }
}