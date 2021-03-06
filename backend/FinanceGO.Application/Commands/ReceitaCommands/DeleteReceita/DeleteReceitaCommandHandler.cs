using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FinanceGO.Core.Authorization;
using FinanceGO.Core.Repositories.ReceitaRepositories;
using FinanceGO.Core.Results;
using MediatR;

namespace FinanceGO.Application.Commands.ReceitaCommands.DeleteReceita
{
    public class DeleteReceitaCommandHandler : IRequestHandler<DeleteReceitaCommand, Result>
    {
        private readonly IReceitaQueryRepository _queryRepository;
        private readonly IReceitaCommandRepository _commandRepository;
        private readonly IMesmoUsuarioAuthorizationRequirement _requirement;

        public DeleteReceitaCommandHandler(IReceitaQueryRepository queryRepository, IReceitaCommandRepository commandRepository, IMesmoUsuarioAuthorizationRequirement requirement)
        {
            _queryRepository = queryRepository;
            _commandRepository = commandRepository;
            _requirement = requirement;
        }
        public async Task<Result> Handle(DeleteReceitaCommand request, CancellationToken cancellationToken)
        {
            var receitaASerDeletada = await _queryRepository.GetReceitaByIdAsync(request.Id);
            if(receitaASerDeletada == null) return new RegistroNaoEncontradoResult();

            var usuarioAutorizado = _requirement.VerificarReceitaMesmoUsuario(receitaASerDeletada);
            if(!usuarioAutorizado) return new UsuarioNaoAutorizadoResult();

            await _commandRepository.DeleteReceitaAsync(receitaASerDeletada);
            return new DeletadoComSucessoResult();
        }
    }
}