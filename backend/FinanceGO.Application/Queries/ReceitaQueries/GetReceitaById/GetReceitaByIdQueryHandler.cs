using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FinanceGO.Application.ViewModels;
using FinanceGO.Core.Repositories.ReceitaRepositories;
using MediatR;

namespace FinanceGO.Application.Queries.ReceitaQueries.GetReceitaById
{
    public class GetReceitaByIdQueryHandler : IRequestHandler<GetReceitaByIdQuery, ReceitaViewModel>
    {
        private readonly IReceitaQueryRepository _queryRepository;
        private readonly IMapper _mapper;

        public GetReceitaByIdQueryHandler(IReceitaQueryRepository queryRepository, IMapper mapper)
        {
            _queryRepository = queryRepository;
            _mapper = mapper;
        }

        public async Task<ReceitaViewModel> Handle(GetReceitaByIdQuery request, CancellationToken cancellationToken)
        {
            var receita = await _queryRepository.GetReceitaByIdAsync(request.Id);
            
            return _mapper.Map<ReceitaViewModel>(receita);
        }
    }
}