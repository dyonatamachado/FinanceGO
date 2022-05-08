using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceGO.Core.Entities;

namespace FinanceGO.Core.Repositories.ReceitaRepositories
{
    public interface IReceitaCommandRepository
    {
        Task CreateReceitaAsync(Receita receita);
        Task SaveChangesAsync();
        Task UpdateReceitaAsync(Receita receitaComDadosAlterados);
        Task DeleteReceitaAsync(Receita receitaASerDeletada);
    }
}