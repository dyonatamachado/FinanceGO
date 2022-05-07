using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceGO.Core.Entities;
using FinanceGO.Core.Repositories.DespesaRepositories;
using Microsoft.EntityFrameworkCore;

namespace FinanceGO.Infrastructure.Persistence.Repositories.DespesaRepositories
{
    public class DespesaCommandRepository : IDespesaCommandRepository
    {
        private readonly FinanceGODbContext _context;

        public DespesaCommandRepository(FinanceGODbContext context)
        {
            _context = context;
        }

        public async Task CreateDespesaAsync(Despesa despesa)
        {
            await _context.Despesas.AddAsync(despesa);
            await SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDespesaAsync(Despesa despesaComDadosAlterados)
        {
            var despesaASerAtualizada = await _context.Despesas
                .SingleOrDefaultAsync(d => d.Id == despesaComDadosAlterados.Id);

            despesaASerAtualizada = despesaComDadosAlterados;

            await SaveChangesAsync();
        }
    }
}