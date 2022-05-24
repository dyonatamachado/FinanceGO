using FinanceGO.Core.Repositories.ReceitaRepositories;
using FinanceGO.Core.RulesValidators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
