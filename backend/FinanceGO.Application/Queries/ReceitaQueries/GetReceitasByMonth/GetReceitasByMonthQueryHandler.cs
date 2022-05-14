using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FinanceGO.Application.ViewModels;
using FinanceGO.Core.Repositories.ReceitaRepositories;
using FinanceGO.Core.UserServices;
using MediatR;

namespace FinanceGO.Application.Queries.ReceitaQueries.GetReceitasByMonth
{
    public class GetReceitasByMonthQueryHandler : IRequestHandler<GetReceitasByMonthQuery, List<ReceitaViewModel>>
    {
        private readonly IReceitaQueryRepository _queryRepository;
        private readonly IMapper _mapper;
        private readonly int _loggedUserId;

        public GetReceitasByMonthQueryHandler(IReceitaQueryRepository queryRepository, IMapper mapper, ILoggedUserService usuarioService)
        {
            _queryRepository = queryRepository;
            _mapper = mapper;
            _loggedUserId = usuarioService.GetUserId();
        }

        public async Task<List<ReceitaViewModel>> Handle(GetReceitasByMonthQuery request, CancellationToken cancellationToken)
        {
            var receitasDoMes = await _queryRepository.GetReceitasByMonthAndUserAsync(request.Mes, request.Ano, _loggedUserId);

            return _mapper.Map<List<ReceitaViewModel>>(receitasDoMes);
        }
    }
}