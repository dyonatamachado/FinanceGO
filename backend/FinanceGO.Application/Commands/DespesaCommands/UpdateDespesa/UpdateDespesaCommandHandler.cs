using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
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

        public UpdateDespesaCommandHandler(IDespesaQueryRepository queryRepository, IDespesaCommandRepository commandRepository, IMapper mapper)
        {
            _queryRepository = queryRepository;
            _commandRepository = commandRepository;
            _mapper = mapper;
        }

        public async Task<Result> Handle(UpdateDespesaCommand request, CancellationToken cancellationToken)
        {
            var despesaASerAlterada = await _queryRepository.GetDespesaByIdAsync(request.Id);
            if(despesaASerAlterada == null) return new RegistroNaoEncontradoResult();
            
            var despesaIsDuplicada = await VerificarSeDespesaDuplicada(request);
            if(despesaIsDuplicada) return new RegistroDuplicadoResult();

            var despesaComDadosAlterados = _mapper.Map(request, despesaASerAlterada);
            await _commandRepository.UpdateDespesaAsync(despesaComDadosAlterados);
            return new RegistroAtualizadoComSucesso();
        }

        private async Task<bool> VerificarSeDespesaDuplicada(UpdateDespesaCommand request)
        {
            var despesasDoMesmoMes = await _queryRepository.GetDespesasByMonthAsync(request.Data.Month, request.Data.Year);
            return despesasDoMesmoMes.Exists(d => d.Descricao == request.Descricao);
        }   
    }
}