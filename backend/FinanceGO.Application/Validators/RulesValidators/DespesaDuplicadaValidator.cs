using FinanceGO.Core.Entities;
using FinanceGO.Core.Repositories.DespesaRepositories;
using FinanceGO.Core.RulesValidators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<bool> DespesaIsDuplicada(DateTime data, string descricao, int userId)
        {
            var despesasDoMesmoMes = await _repository.GetDespesasByMonthAndUserAsync(data.Month, data.Year, userId);

            return despesasDoMesmoMes.Exists(d => d.Descricao == descricao);
        }
    }
}
