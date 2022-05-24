using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceGO.Core.RulesValidators
{
    public interface IReceitaDuplicadaValidator
    {
        Task<bool> ReceitaIsDuplicada(DateTime data, string descricao, int userId);
    }
}
