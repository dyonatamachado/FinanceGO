using FinanceGO.Application.Commands.ReceitaCommands.CreateReceita;
using FinanceGO.Application.Commands.ReceitaCommands.UpdateReceita;
using FinanceGO.Application.Validators.IRulesValidators;
using FinanceGO.Core.Repositories.ReceitaRepositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceGO.Application.Validators.RulesValidators
{
    public class ReceitaDuplicadaValidator : IReceitaDuplicadaValidator
    {
        private readonly IReceitaQueryRepository _repository;

        public ReceitaDuplicadaValidator(IReceitaQueryRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> ReceitaIsDuplicada(DateTime data, string descricao, int userId)
        {
            var receitasDoMesmoMes = await _repository.GetReceitasByMonthAndUserAsync(data.Month, data.Year, userId);
            
            return receitasDoMesmoMes.Exists(r => r.Descricao == descricao);
        }

        public async Task<bool> ReceitaIsDuplicada(CreateReceitaCommand command)
        {
            var receitasDoMesmoMes = await _repository.GetReceitasByMonthAndUserAsync(command.Data.Month, command.Data.Year, command.UsuarioId);

            return receitasDoMesmoMes.Exists(r => r.Descricao == command.Descricao);
        }

        public async Task<bool> ReceitaIsDuplicada(UpdateReceitaCommand command, int loggedUserId)
        {
            var receitasDoMesmoMes = await _repository.GetReceitasByMonthAndUserAsync(command.Data.Month, command.Data.Year, loggedUserId);

            var possivelReceitaDuplicada = receitasDoMesmoMes.SingleOrDefault(r => r.Descricao == command.Descricao);

            if (possivelReceitaDuplicada == null)
                return false;

            if (possivelReceitaDuplicada.Id == command.Id)
                return false;
            else
                return true;
        }
    }
}
