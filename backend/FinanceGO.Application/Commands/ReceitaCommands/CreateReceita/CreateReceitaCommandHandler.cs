using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FinanceGO.Application.Validators.IRulesValidators;
using FinanceGO.Application.ViewModels;
using FinanceGO.Core.Entities;
using FinanceGO.Core.Repositories.ReceitaRepositories;
using FinanceGO.Core.Results;
using MediatR;

namespace FinanceGO.Application.Commands.ReceitaCommands.CreateReceita
{
    public class CreateReceitaCommandHandler : IRequestHandler<CreateReceitaCommand, Result>
    {
        private readonly IReceitaCommandRepository _repository;
        private readonly IMapper _mapper;
        private readonly IReceitaDuplicadaValidator _validator;

        public CreateReceitaCommandHandler(IReceitaCommandRepository repository, IMapper mapper, IReceitaDuplicadaValidator validator)
        {
            _repository = repository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<Result> Handle(CreateReceitaCommand request, CancellationToken cancellationToken)
        {
            var receitaIsDuplicada = await _validator.ReceitaIsDuplicada(request);
            if(receitaIsDuplicada) return new RegistroDuplicadoResult();

            var receita = _mapper.Map<Receita>(request);
            await _repository.CreateReceitaAsync(receita);

            var receitaViewModel = _mapper.Map<ReceitaViewModel>(receita);
            return new CriadoComSucessoResult(receitaViewModel);
        }
    }
}