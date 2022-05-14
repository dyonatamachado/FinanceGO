using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FinanceGO.Application.ViewModels;
using FinanceGO.Core.Authorization;
using FinanceGO.Core.Repositories.ReceitaRepositories;
using FinanceGO.Core.Results;
using MediatR;

namespace FinanceGO.Application.Queries.ReceitaQueries.GetReceitaById
{
    public class GetReceitaByIdQueryHandler : IRequestHandler<GetReceitaByIdQuery, Result>
    {
        private readonly IReceitaQueryRepository _queryRepository;
        private readonly IMapper _mapper;
        private readonly IMesmoUsuarioAuthorizationRequirement _requirement;

        public GetReceitaByIdQueryHandler(IReceitaQueryRepository queryRepository, IMapper mapper, IMesmoUsuarioAuthorizationRequirement requirement)
        {
            _queryRepository = queryRepository;
            _mapper = mapper;
            _requirement = requirement;
        }

        public async Task<Result> Handle(GetReceitaByIdQuery request, CancellationToken cancellationToken)
        {
            var receita = await _queryRepository.GetReceitaByIdAsync(request.Id);
            if(receita == null) return new RegistroNaoEncontradoResult();

            var usuarioAutorizado = _requirement.VerificarReceitaMesmoUsuario(receita);
            if(!usuarioAutorizado) return new UsuarioNaoAutorizadoResult();

            var receitaViewModel = _mapper.Map<ReceitaViewModel>(receita);
            return new RegistroEncontradoResult(receitaViewModel);
        }
    }
}