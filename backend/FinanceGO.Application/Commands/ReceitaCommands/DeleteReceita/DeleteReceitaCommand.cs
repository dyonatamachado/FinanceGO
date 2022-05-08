using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceGO.Core.Results;
using MediatR;

namespace FinanceGO.Application.Commands.ReceitaCommands.DeleteReceita
{
    public class DeleteReceitaCommand : IRequest<Result>
    {
        public DeleteReceitaCommand(int id)
        {
            Id = id;
        }

        public int Id { get; private set; } 
    }
}