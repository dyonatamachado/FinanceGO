using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceGO.Core.Entities;

namespace FinanceGO.Core.Repositories.ReceitaRepositories
{
    public interface IReceitaQueryRepository
    {
        Task<Receita> GetReceitaByIdAsync(int id);
        Task<List<Receita>> GetReceitasByUserAsync(int userId);
        Task<List<Receita>> GetReceitasByMonthAndUserAsync(int mes, int ano, int userId);
    }
}