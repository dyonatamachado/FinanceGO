using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FinanceGO.Application.ViewModels;
using FinanceGO.Core.Repositories.ReceitaRepositories;
using MediatR;

namespace FinanceGO.Application.Queries.ReceitaQueries.GetReceitas
{
    public class GetReceitasQueryHandler : IRequestHandler<GetReceitasQuery, List<ReceitaViewModel>>
    {
        private readonly IReceitaQueryRepository _queryRepository;
        private readonly IMapper _mapper;

        public GetReceitasQueryHandler(IReceitaQueryRepository queryRepository, IMapper mapper)
        {
            _queryRepository = queryRepository;
            _mapper = mapper;
        }

        public async Task<List<ReceitaViewModel>> Handle(GetReceitasQuery request, CancellationToken cancellationToken)
        {
            var receitas = await _queryRepository.GetReceitasAsync();

            return _mapper.Map<List<ReceitaViewModel>>(receitas);
        }
    }
}