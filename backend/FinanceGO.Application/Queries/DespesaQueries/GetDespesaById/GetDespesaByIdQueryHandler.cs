using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FinanceGO.Application.ViewModels;
using FinanceGO.Core.Authorization;
using FinanceGO.Core.Repositories.DespesaRepositories;
using FinanceGO.Core.Results;
using MediatR;

namespace FinanceGO.Application.Queries.DespesaQueries.GetDespesaById
{
    public class GetDespesaByIdQueryHandler : IRequestHandler<GetDespesaByIdQuery, Result>
    {   
        private readonly IDespesaQueryRepository _queryRepository;
        private readonly IMapper _mapper;
        private readonly IMesmoUsuarioAuthorizationRequirement _requirement;

        public GetDespesaByIdQueryHandler(IDespesaQueryRepository queryRepository, IMapper mapper, IMesmoUsuarioAuthorizationRequirement requirement)
        {
            _queryRepository = queryRepository;
            _mapper = mapper;
            _requirement = requirement;
        }

        public async Task<Result> Handle(GetDespesaByIdQuery request, CancellationToken cancellationToken)
        {
            var despesa = await _queryRepository.GetDespesaByIdAsync(request.Id);
            if(despesa == null) return new RegistroNaoEncontradoResult();

            var usuarioAutorizado = _requirement.VerificarDespesaMesmoUsuario(despesa);
            if(!usuarioAutorizado) return new UsuarioNaoAutorizadoResult();

            var despesaViewModel = _mapper.Map<DespesaViewModel>(despesa);
            return new RegistroEncontradoResult(despesaViewModel);
        }
    }
}