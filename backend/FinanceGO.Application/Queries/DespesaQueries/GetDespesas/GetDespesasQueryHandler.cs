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

namespace FinanceGO.Application.Queries.DespesaQueries.GetDespesas
{
    public class GetDespesasQueryHandler : IRequestHandler<GetDespesasQuery, List<DespesaViewModel>>
    {
        private readonly IDespesaQueryRepository _queryRepository;
        private readonly IMapper _mapper;
        private readonly int _loggedUserId;

        public GetDespesasQueryHandler(IDespesaQueryRepository queryRepository, IMapper mapper, ILoggedUserService usuarioService)
        {
            _queryRepository = queryRepository;
            _mapper = mapper;
            _loggedUserId = usuarioService.GetUserId();
        }

        public async Task<List<DespesaViewModel>> Handle(GetDespesasQuery request, CancellationToken cancellationToken)
        {
            var despesas = await _queryRepository.GetDespesasByUserAsync(_loggedUserId);

            return _mapper.Map<List<DespesaViewModel>>(despesas);
        }
    }
}