using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceGO.Core.Results;
using MediatR;

namespace FinanceGO.Application.Commands.DespesaCommands.DeleteDespesa
{
    public class DeleteDespesaCommand : IRequest<Result>
    {
        public int Id { get; private set; }

        public DeleteDespesaCommand(int id)
        {
            Id = id;
        }
    }
}