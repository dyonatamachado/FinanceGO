using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FinanceGO.Application.ViewModels;
using FinanceGO.Core.Entities;
using FinanceGO.Core.Repositories.DespesaRepositories;
using MediatR;

namespace FinanceGO.Application.Queries.DespesaQueries.ReadDespesaById
{
    public class ReadDespesaByIdQueryHandler : IRequestHandler<ReadDespesaByIdQuery, DespesaViewModel>
    {
        private readonly IMapper _mapper;
        private readonly IDespesaQueryRepository _despesaQueryRepository;

        public ReadDespesaByIdQueryHandler(IMapper mapper, IDespesaQueryRepository despesaQueryRepository)
        {
            _mapper = mapper;
            _despesaQueryRepository = despesaQueryRepository;
        }
        public async Task<DespesaViewModel> Handle(ReadDespesaByIdQuery request, CancellationToken cancellationToken)
        {
            var despesa = await _despesaQueryRepository.ReadDespesaByIdAsync(request.Id);

            if(despesa == null)
                return null;
            
            return _mapper.Map<DespesaViewModel>(despesa);
        }
    }
}