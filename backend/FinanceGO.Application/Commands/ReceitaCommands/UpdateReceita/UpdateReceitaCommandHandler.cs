using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FinanceGO.Core.Authorization;
using FinanceGO.Core.Repositories.ReceitaRepositories;
using FinanceGO.Core.Results;
using FinanceGO.Core.RulesValidators;
using MediatR;

namespace FinanceGO.Application.Commands.ReceitaCommands.UpdateReceita
{
    public class UpdateReceitaCommandHandler : IRequestHandler<UpdateReceitaCommand, Result>
    {
        private readonly IReceitaQueryRepository _queryRepository;
        private readonly IReceitaCommandRepository _commandRepository;
        private readonly IMapper _mapper;
        private readonly IMesmoUsuarioAuthorizationRequirement _requirement;
        private readonly IReceitaDuplicadaValidator _validator;

        public UpdateReceitaCommandHandler(IReceitaQueryRepository queryRepository, IReceitaCommandRepository commandRepository, IMapper mapper, IMesmoUsuarioAuthorizationRequirement requirement, IReceitaDuplicadaValidator validator)
        {
            _queryRepository = queryRepository;
            _commandRepository = commandRepository;
            _mapper = mapper;
            _requirement = requirement;
            _validator = validator;
        }

        public async Task<Result> Handle(UpdateReceitaCommand request, CancellationToken cancellationToken)
        {
            var receitaASerAlterada = await _queryRepository.GetReceitaByIdAsync(request.Id);
            if(receitaASerAlterada == null) return new RegistroNaoEncontradoResult();

            var usuarioAutorizado = _requirement.VerificarReceitaMesmoUsuario(receitaASerAlterada);
            if(!usuarioAutorizado) return new UsuarioNaoAutorizadoResult();

            var receitaIsDuplicada = await _validator.ReceitaIsDuplicada(request.Data, request.Descricao, _requirement.GetUserId());
            if(receitaIsDuplicada) return new RegistroDuplicadoResult();

            var receitaComDadosAlterados = _mapper.Map(request, receitaASerAlterada);
            await _commandRepository.UpdateReceitaAsync(receitaComDadosAlterados);
            return new RegistroAtualizadoComSucessoResult();
        }
    }
}