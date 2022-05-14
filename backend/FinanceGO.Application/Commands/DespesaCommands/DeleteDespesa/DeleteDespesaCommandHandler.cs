using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FinanceGO.Core.Authorization;
using FinanceGO.Core.Repositories.DespesaRepositories;
using FinanceGO.Core.Results;
using MediatR;

namespace FinanceGO.Application.Commands.DespesaCommands.DeleteDespesa
{
    public class DeleteDespesaCommandHandler : IRequestHandler<DeleteDespesaCommand, Result>
    {
        private readonly IDespesaCommandRepository _commandRepository;
        private readonly IDespesaQueryRepository _queryRepository;
        private readonly IMesmoUsuarioAuthorizationRequirement _requirement;

        public DeleteDespesaCommandHandler(IDespesaCommandRepository commandRepository, IDespesaQueryRepository queryRepository, IMesmoUsuarioAuthorizationRequirement requirement)
        {
            _commandRepository = commandRepository;
            _queryRepository = queryRepository;
            _requirement = requirement;
        }

        public async Task<Result> Handle(DeleteDespesaCommand request, CancellationToken cancellationToken)
        {
            var despesaASerDeletada = await _queryRepository.GetDespesaByIdAsync(request.Id);
            if(despesaASerDeletada == null) return new RegistroNaoEncontradoResult();

            var usuarioAutorizado = _requirement.VerificarDespesaMesmoUsuario(despesaASerDeletada);
            if(!usuarioAutorizado) return new UsuarioNaoAutorizadoResult();

            await _commandRepository.DeleteDespesaAsync(despesaASerDeletada);
            return new DeletadoComSucessoResult();
        }
    }
}