using FinanceGO.Application.Commands.ReceitaCommands.CreateReceita;
using FinanceGO.Application.Commands.ReceitaCommands.UpdateReceita;
using System.Threading.Tasks;

namespace FinanceGO.Application.Validators.IRulesValidators
{
    public interface IReceitaDuplicadaValidator
    {
        Task<bool> ReceitaIsDuplicada(CreateReceitaCommand command);
        Task<bool> ReceitaIsDuplicada(UpdateReceitaCommand command, int loggedUserId);
    }
}
