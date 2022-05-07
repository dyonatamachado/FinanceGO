using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceGO.Core.Entities;
using FinanceGO.Core.Repositories.ReceitaRepositories;
using Microsoft.EntityFrameworkCore;

namespace FinanceGO.Infrastructure.Persistence.Repositories.ReceitaRepositories
{
    public class ReceitaCommandRepository : IReceitaCommandRepository
    {
        private readonly FinanceGODbContext _context;

        public ReceitaCommandRepository(FinanceGODbContext context)
        {
            _context = context;
        }

        public async Task CreateReceitaAsync(Receita receita)
        {
            await _context.Receitas.AddAsync(receita);
            await SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateReceitaAsync(Receita receitaComDadosAlterados)
        {
            var receitaASerAtualizada = await _context.Receitas
                .SingleOrDefaultAsync(r => r.Id == receitaComDadosAlterados.Id);

            receitaASerAtualizada = receitaComDadosAlterados;

            await SaveChangesAsync();
        }
    }
}