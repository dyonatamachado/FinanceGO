using FinanceGO.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceGO.Core.RulesValidators
{
    public interface IDespesaDuplicadaValidator
    {
        Task<bool> DespesaIsDuplicada(DateTime data, string descricao, int userId);
    }
}
