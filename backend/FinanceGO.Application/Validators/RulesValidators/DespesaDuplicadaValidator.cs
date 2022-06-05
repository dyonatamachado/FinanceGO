using FinanceGO.Application.Commands.DespesaCommands.CreateDespesa;
using FinanceGO.Application.Commands.DespesaCommands.UpdateDespesa;
using FinanceGO.Application.Validators.IRulesValidators;
using FinanceGO.Core.Repositories.DespesaRepositories;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceGO.Application.Validators.RulesValidators
{
    public class DespesaDuplicadaValidator : IDespesaDuplicadaValidator
    {
        private readonly IDespesaQueryRepository _repository;
        public DespesaDuplicadaValidator(IDespesaQueryRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> DespesaIsDuplicada(UpdateDespesaCommand command, int loggedUserId)
        {
            var despesasDoMesmoMes = await _repository.GetDespesasByMonthAndUserAsync(command.Data.Month, command.Data.Year, loggedUserId);

            var possivelDespesaDuplicada = despesasDoMesmoMes.SingleOrDefault(d => d.Descricao == command.Descricao);

            if (possivelDespesaDuplicada == null)
                return false;

            if (possivelDespesaDuplicada.Id == command.Id) 
                return false;
            else 
                return true;
        }

        public async Task<bool> DespesaIsDuplicada(CreateDespesaCommand command)
        {
            var despesasDoMesmoMes = await _repository.GetDespesasByMonthAndUserAsync(command.Data.Month, command.Data.Year, command.UsuarioId);

            return despesasDoMesmoMes.Exists(d => d.Descricao == command.Descricao);
        }
    }
}
