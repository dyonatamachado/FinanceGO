using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FinanceGO.Core.Repositories.ReceitaRepositories;
using FinanceGO.Core.Results;
using MediatR;

namespace FinanceGO.Application.Commands.ReceitaCommands.UpdateReceita
{
    public class UpdateReceitaCommandHandler : IRequestHandler<UpdateReceitaCommand, Result>
    {
        private readonly IReceitaQueryRepository _queryRepository;
        private readonly IReceitaCommandRepository _commandRepository;
        private readonly IMapper _mapper;

        public UpdateReceitaCommandHandler(IReceitaQueryRepository queryRepository, IReceitaCommandRepository commandRepository, IMapper mapper)
        {
            _queryRepository = queryRepository;
            _commandRepository = commandRepository;
            _mapper = mapper;
        }

        public async Task<Result> Handle(UpdateReceitaCommand request, CancellationToken cancellationToken)
        {
            var receitaASerAlterada = await _queryRepository.GetReceitaByIdAsync(request.Id);
            if(receitaASerAlterada == null) return new RegistroNaoEncontradoResult();

            var receitaIsDuplicada = await VerificarSeReceitaDuplicada(request);
            if(receitaIsDuplicada) return new RegistroDuplicadoResult();

            var receitaComDadosAlterados = _mapper.Map(request, receitaASerAlterada);
            await _commandRepository.UpdateReceitaAsync(receitaComDadosAlterados);
            return new RegistroAtualizadoComSucesso();
        }

        private async Task<bool> VerificarSeReceitaDuplicada(UpdateReceitaCommand request)
        {
            var receitasDoMesmoMes = await _queryRepository
                .GetReceitasByMonthAsync(request.Data.Month, request.Data.Year);
            
            return receitasDoMesmoMes.Exists(r => r.Descricao == request.Descricao);
        }
    }
}