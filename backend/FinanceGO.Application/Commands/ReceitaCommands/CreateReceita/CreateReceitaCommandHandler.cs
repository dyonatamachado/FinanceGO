using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FinanceGO.Application.ViewModels;
using FinanceGO.Core.Entities;
using FinanceGO.Core.Repositories.ReceitaRepositories;
using FinanceGO.Core.Results;
using MediatR;

namespace FinanceGO.Application.Commands.ReceitaCommands.CreateReceita
{
    public class CreateReceitaCommandHandler : IRequestHandler<CreateReceitaCommand, Result>
    {
        private readonly IReceitaQueryRepository _queryRepository;
        private readonly IReceitaCommandRepository _commandRepository;
        private readonly IMapper _mapper;

        public CreateReceitaCommandHandler(IReceitaQueryRepository queryRepository, IReceitaCommandRepository commandRepository, IMapper mapper)
        {
            _queryRepository = queryRepository;
            _commandRepository = commandRepository;
            _mapper = mapper;
        }

        public async Task<Result> Handle(CreateReceitaCommand request, CancellationToken cancellationToken)
        {
            var receitaIsDuplicada = await VerificarSeReceitaDuplicada(request);
            if(receitaIsDuplicada) return new RegistroDuplicadoResult();

            var receita = _mapper.Map<Receita>(request);
            await _commandRepository.CreateReceitaAsync(receita);

            var receitaViewModel = _mapper.Map<ReceitaViewModel>(receita);
            return new CriadoComSucessoResult(receitaViewModel);
        }

        private async Task<bool> VerificarSeReceitaDuplicada(CreateReceitaCommand request)
        {
            var receitasDoMesmoMes = await _queryRepository
                .GetReceitasByMonthAndUserAsync(request.Data.Month, request.Data.Year, request.UsuarioId);
            
            return receitasDoMesmoMes.Exists(r => r.Descricao == request.Descricao);
        }
    }
}