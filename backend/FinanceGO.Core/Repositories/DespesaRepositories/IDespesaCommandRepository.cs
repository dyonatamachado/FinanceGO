using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceGO.Core.Entities;

namespace FinanceGO.Core.Repositories.DespesaRepositories
{
    public interface IDespesaCommandRepository
    {
        Task CreateDespesaAsync(Despesa despesa);
        Task SaveChangesAsync();
        Task UpdateDespesaAsync(Despesa despesaComDadosAlterados);
        Task DeleteDespesaAsync(Despesa despesaASerDeletada);
    }
}