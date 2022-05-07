using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FinanceGO.Application.ViewModels;
using FinanceGO.Core.Entities;
using FinanceGO.Core.Repositories.DespesaRepositories;
using FinanceGO.Core.Results;
using MediatR;

namespace FinanceGO.Application.Commands.DespesaCommands.CreateDespesa
{
    public class CreateDespesaCommandHandler : IRequestHandler<CreateDespesaCommand, Result>
    {
        private readonly IDespesaCommandRepository _commandRepository;
        private readonly IDespesaQueryRepository _queryRepository;
        private readonly IMapper _mapper;

        public CreateDespesaCommandHandler(IDespesaCommandRepository commandRepository, IDespesaQueryRepository queryRepository, IMapper mapper)
        {
            _commandRepository = commandRepository;
            _queryRepository = queryRepository;
            _mapper = mapper;
        }

        public async Task<Result> Handle(CreateDespesaCommand request, CancellationToken cancellationToken)
        {
            var despesaIsDuplicada = await VerificarSeDespesaDuplicada(request);
            if(despesaIsDuplicada) return new RegistroDuplicadoResult();

            var despesa = _mapper.Map<Despesa>(request);

            await _commandRepository.CreateDespesaAsync(despesa);

            var despesaViewModel = _mapper.Map<DespesaViewModel>(despesa);

            return new CriadoComSucessoResult(despesaViewModel);
        }

        private async Task<bool> VerificarSeDespesaDuplicada(CreateDespesaCommand request)
        {
            var despesasDoMesmoMes = await _queryRepository
                .GetDespesasByMonthAsync(request.Data.Month, request.Data.Year);

            return despesasDoMesmoMes.Exists(d => d.Descricao == request.Descricao);
        }
    }
}