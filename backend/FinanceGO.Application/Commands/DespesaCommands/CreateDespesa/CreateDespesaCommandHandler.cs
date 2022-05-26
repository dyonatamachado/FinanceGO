using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FinanceGO.Application.Validators.IRulesValidators;
using FinanceGO.Application.ViewModels;
using FinanceGO.Core.Entities;
using FinanceGO.Core.Repositories.DespesaRepositories;
using FinanceGO.Core.Results;
using MediatR;

namespace FinanceGO.Application.Commands.DespesaCommands.CreateDespesa
{
    public class CreateDespesaCommandHandler : IRequestHandler<CreateDespesaCommand, Result>
    {
        private readonly IDespesaCommandRepository _repository;
        private readonly IMapper _mapper;
        private readonly IDespesaDuplicadaValidator _validator;

        public CreateDespesaCommandHandler(IDespesaCommandRepository repository, IMapper mapper, IDespesaDuplicadaValidator validator)
        {
            _repository = repository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<Result> Handle(CreateDespesaCommand request, CancellationToken cancellationToken)
        {
            var despesaIsDuplicada = await _validator.DespesaIsDuplicada(request);
            if(despesaIsDuplicada) return new RegistroDuplicadoResult();

            var despesa = _mapper.Map<Despesa>(request);
            await _repository.CreateDespesaAsync(despesa);

            var despesaViewModel = _mapper.Map<DespesaViewModel>(despesa);
            return new CriadoComSucessoResult(despesaViewModel);
        }
    }
}