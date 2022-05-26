using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FinanceGO.Core.Authorization;
using FinanceGO.Core.Repositories.DespesaRepositories;
using FinanceGO.Core.Results;
using FinanceGO.Core.RulesValidators;
using MediatR;

namespace FinanceGO.Application.Commands.DespesaCommands.UpdateDespesa
{
    public class UpdateDespesaCommandHandler : IRequestHandler<UpdateDespesaCommand, Result>
    {
        private readonly IDespesaQueryRepository _queryRepository;
        private readonly IDespesaCommandRepository _commandRepository;
        private readonly IMapper _mapper;
        private readonly IMesmoUsuarioAuthorizationRequirement _requirement;
        private readonly IDespesaDuplicadaValidator _validator;

        public UpdateDespesaCommandHandler(IDespesaQueryRepository queryRepository, IDespesaCommandRepository commandRepository, IMapper mapper, IMesmoUsuarioAuthorizationRequirement requirement, IDespesaDuplicadaValidator validator)
        {
            _queryRepository = queryRepository;
            _commandRepository = commandRepository;
            _mapper = mapper;
            _requirement = requirement;
            _validator = validator;
        }

        public async Task<Result> Handle(UpdateDespesaCommand request, CancellationToken cancellationToken)
        {
            var despesaASerAlterada = await _queryRepository.GetDespesaByIdAsync(request.Id);
            if(despesaASerAlterada == null) return new RegistroNaoEncontradoResult();

            var usuarioAutorizado = _requirement.VerificarDespesaMesmoUsuario(despesaASerAlterada);
            if(!usuarioAutorizado) return new UsuarioNaoAutorizadoResult();
            
            var despesaIsDuplicada = await _validator.DespesaIsDuplicada(request.Data, request.Descricao, _requirement.GetUserId());
            if(despesaIsDuplicada) return new RegistroDuplicadoResult();

            var despesaComDadosAlterados = _mapper.Map(request, despesaASerAlterada);
            await _commandRepository.UpdateDespesaAsync(despesaComDadosAlterados);
            return new RegistroAtualizadoComSucessoResult();
        }
    }
}