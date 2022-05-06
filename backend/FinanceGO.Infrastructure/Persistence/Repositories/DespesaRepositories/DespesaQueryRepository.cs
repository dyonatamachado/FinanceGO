using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceGO.Core.Entities;
using FinanceGO.Core.Repositories.DespesaRepositories;

namespace FinanceGO.Infrastructure.Persistence.Repositories.DespesaRepositories
{
    public class DespesaQueryRepository : IDespesaQueryRepository
    {
        public Task<Despesa> ReadDespesaByMonthAsync(int mes, int ano)
        {
            throw new NotImplementedException();
        }
    }
}