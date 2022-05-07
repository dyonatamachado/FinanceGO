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
        Task<List<Receita>> GetReceitasAsync();
        Task<List<Receita>> GetReceitasByMonthAsync(int mes, int ano);
    }
}