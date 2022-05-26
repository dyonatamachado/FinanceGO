using FinanceGO.Application.Commands.DespesaCommands.CreateDespesa;
using FinanceGO.Application.Commands.DespesaCommands.UpdateDespesa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceGO.Application.Validators.IRulesValidators
{
    public interface IDespesaDuplicadaValidator
    {
        Task<bool> DespesaIsDuplicada(CreateDespesaCommand command);
        Task<bool> DespesaIsDuplicada(UpdateDespesaCommand command, int loggedUserId);

    }
}
