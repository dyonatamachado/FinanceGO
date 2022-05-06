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
        private readonly IDespesaCommandRepository _despesaCommandRepository;
        private readonly IDespesaQueryRepository _despesaQueryRepository;
        private readonly IMapper _mapper;

        public CreateDespesaCommandHandler(IDespesaCommandRepository despesaCommandRepository, IMapper mapper, IDespesaQueryRepository despesaQueryRepository)
        {
            _despesaCommandRepository = despesaCommandRepository;
            _mapper = mapper;
            _despesaQueryRepository = despesaQueryRepository;
        }

        public async Task<Result> Handle(CreateDespesaCommand request, CancellationToken cancellationToken)
        {
            var possivelDespesaDuplicada = await _despesaQueryRepository
                .ReadDespesaByMonthAsync(request.Data.Month, request.Data.Year);

            if(possivelDespesaDuplicada != null)
                return new RegistroDuplicadoResult();
            
            var despesa = _mapper.Map<Despesa>(request);
            await _despesaCommandRepository.CreateDespesaAsync(despesa);

            var despesaViewModel = _mapper.Map<DespesaViewModel>(despesa);
            return new CriadoComSucessoResult(despesaViewModel);
        }
    }
}