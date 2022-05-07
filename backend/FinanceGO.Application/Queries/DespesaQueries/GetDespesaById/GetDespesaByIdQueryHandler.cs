using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FinanceGO.Application.ViewModels;
using FinanceGO.Core.Repositories.DespesaRepositories;
using MediatR;

namespace FinanceGO.Application.Queries.DespesaQueries.GetDespesaById
{
    public class GetDespesaByIdQueryHandler : IRequestHandler<GetDespesaByIdQuery, DespesaViewModel>
    {   
        private readonly IDespesaQueryRepository _queryRepository;
        private readonly IMapper _mapper;

        public GetDespesaByIdQueryHandler(IDespesaQueryRepository queryRepository, IMapper mapper)
        {
            _queryRepository = queryRepository;
            _mapper = mapper;
        }

        public async Task<DespesaViewModel> Handle(GetDespesaByIdQuery request, CancellationToken cancellationToken)
        {
            var despesa = await _queryRepository.GetDespesaByIdAsync(request.Id);

            return _mapper.Map<DespesaViewModel>(despesa);
        }
    }
}