using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FinanceGO.Core.Repositories.DespesaRepositories;
using FinanceGO.Core.Results;
using MediatR;

namespace FinanceGO.Application.Commands.DespesaCommands.DeleteDespesa
{
    public class DeleteDespesaCommandHandler : IRequestHandler<DeleteDespesaCommand, Result>
    {
        private readonly IDespesaCommandRepository _commandRepository;
        private readonly IDespesaQueryRepository _queryRepository;

        public DeleteDespesaCommandHandler(IDespesaCommandRepository commandRepository, IDespesaQueryRepository queryRepository)
        {
            _commandRepository = commandRepository;
            _queryRepository = queryRepository;
        }

        public async Task<Result> Handle(DeleteDespesaCommand request, CancellationToken cancellationToken)
        {
            var despesaASerDeletada = await _queryRepository.GetDespesaByIdAsync(request.Id);
            if(despesaASerDeletada == null) 
                return new RegistroNaoEncontradoResult();

            await _commandRepository.DeleteDespesaAsync(despesaASerDeletada);
            return new DeletadoComSucessoResult();
        }
    }
}