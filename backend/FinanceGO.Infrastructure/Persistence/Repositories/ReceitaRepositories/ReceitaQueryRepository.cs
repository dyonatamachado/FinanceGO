using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceGO.Core.Entities;
using FinanceGO.Core.Repositories.ReceitaRepositories;
using Microsoft.EntityFrameworkCore;

namespace FinanceGO.Infrastructure.Persistence.Repositories.ReceitaRepositories
{
    public class ReceitaQueryRepository : IReceitaQueryRepository
    {
        private readonly FinanceGODbContext _context;

        public ReceitaQueryRepository(FinanceGODbContext context)
        {
            _context = context;
        }

        public async Task<Receita> GetReceitaByIdAsync(int id)
        {
            return await _context.Receitas.SingleOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<Receita>> GetReceitasAsync()
        {
            return await _context.Receitas.ToListAsync();
        }

        public async Task<List<Receita>> GetReceitasByMonthAsync(int mes, int ano)
        {
            return await _context.Receitas
                .Where(r => r.Data.Month == mes && r.Data.Year == ano)
                .ToListAsync();
        }
    }
}