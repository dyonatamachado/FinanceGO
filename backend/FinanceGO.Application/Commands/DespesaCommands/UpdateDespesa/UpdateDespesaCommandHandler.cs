using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FinanceGO.Core.Authorization;
using FinanceGO.Core.Repositories.DespesaRepositories;
using FinanceGO.Core.Results;
using MediatR;

namespace FinanceGO.Application.Commands.DespesaCommands.UpdateDespesa
{
    public class UpdateDespesaCommandHandler : IRequestHandler<UpdateDespesaCommand, Result>
    {
        private readonly IDespesaQueryRepository _queryRepository;
        private readonly IDespesaCommandRepository _commandRepository;
        private readonly IMapper _mapper;
        private readonly IMesmoUsuarioAuthorizationRequirement _requirement;

        public UpdateDespesaCommandHandler(IDespesaQueryRepository queryRepository, IDespesaCommandRepository commandRepository, IMapper mapper, IMesmoUsuarioAuthorizationRequirement requirement)
        {
            _queryRepository = queryRepository;
            _commandRepository = commandRepository;
            _mapper = mapper;
            _requirement = requirement;
        }

        public async Task<Result> Handle(UpdateDespesaCommand request, CancellationToken cancellationToken)
        {
            var despesaASerAlterada = await _queryRepository.GetDespesaByIdAsync(request.Id);
            if(despesaASerAlterada == null) return new RegistroNaoEncontradoResult();

            var usuarioAutorizado = _requirement.VerificarDespesaMesmoUsuario(despesaASerAlterada);
            if(!usuarioAutorizado) return new UsuarioNaoAutorizadoResult();
            
            var despesaIsDuplicada = await VerificarSeDespesaDuplicada(request);
            if(despesaIsDuplicada) return new RegistroDuplicadoResult();

            var despesaComDadosAlterados = _mapper.Map(request, despesaASerAlterada);
            await _commandRepository.UpdateDespesaAsync(despesaComDadosAlterados);
            return new RegistroAtualizadoComSucessoResult();
        }

        private async Task<bool> VerificarSeDespesaDuplicada(UpdateDespesaCommand request)
        {
            var despesasDoMesmoMes = await _queryRepository
                .GetDespesasByMonthAndUserAsync(request.Data.Month, request.Data.Year, _requirement.GetUserId());

            return despesasDoMesmoMes.Exists(d => d.Descricao == request.Descricao);
        }   
    }
}