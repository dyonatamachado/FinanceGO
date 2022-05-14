using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FinanceGO.Core.Authorization;
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
        private readonly IMesmoUsuarioAuthorizationRequirement _requirement;

        public UpdateReceitaCommandHandler(IReceitaQueryRepository queryRepository, IReceitaCommandRepository commandRepository, IMapper mapper, IMesmoUsuarioAuthorizationRequirement requirement)
        {
            _queryRepository = queryRepository;
            _commandRepository = commandRepository;
            _mapper = mapper;
            _requirement = requirement;
        }

        public async Task<Result> Handle(UpdateReceitaCommand request, CancellationToken cancellationToken)
        {
            var receitaASerAlterada = await _queryRepository.GetReceitaByIdAsync(request.Id);
            if(receitaASerAlterada == null) return new RegistroNaoEncontradoResult();

            var usuarioAutorizado = _requirement.VerificarReceitaMesmoUsuario(receitaASerAlterada);
            if(!usuarioAutorizado) return new UsuarioNaoAutorizadoResult();

            var receitaIsDuplicada = await VerificarSeReceitaDuplicada(request);
            if(receitaIsDuplicada) return new RegistroDuplicadoResult();

            var receitaComDadosAlterados = _mapper.Map(request, receitaASerAlterada);
            await _commandRepository.UpdateReceitaAsync(receitaComDadosAlterados);
            return new RegistroAtualizadoComSucessoResult();
        }

        private async Task<bool> VerificarSeReceitaDuplicada(UpdateReceitaCommand request)
        {
            var receitasDoMesmoMes = await _queryRepository
                .GetReceitasByMonthAndUserAsync(request.Data.Month, request.Data.Year, _requirement.GetUserId());
            
            return receitasDoMesmoMes.Exists(r => r.Descricao == request.Descricao);
        }
    }
}