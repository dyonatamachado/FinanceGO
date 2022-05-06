using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceGO.Core.Entities;

namespace FinanceGO.Core.Repositories.DespesaRepositories
{
    public interface IDespesaQueryRepository
    {
        Task<Despesa> ReadDespesaByMonthAsync(int mes, int ano);
    }
}