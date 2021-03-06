using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceGO.Core.Entities;

namespace FinanceGO.Core.Repositories.DespesaRepositories
{
    public interface IDespesaQueryRepository
    {
        Task<List<Despesa>> GetDespesasByMonthAndUserAsync(int mes, int ano, int userId);
        Task<Despesa> GetDespesaByIdAsync(int id);
        Task<List<Despesa>> GetDespesasByUserAsync(int userId);
    }
}