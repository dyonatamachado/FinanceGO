using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceGO.Core.Entities;
using FinanceGO.Core.Repositories.DespesaRepositories;
using Microsoft.EntityFrameworkCore;

namespace FinanceGO.Infrastructure.Persistence.Repositories.DespesaRepositories
{
    public class DespesaQueryRepository : IDespesaQueryRepository
    {
        private readonly FinanceGODbContext _context;

        public DespesaQueryRepository(FinanceGODbContext context)
        {
            _context = context;
        }

        public async Task<Despesa> GetDespesaByIdAsync(int id)
        {
            return await _context.Despesas.SingleOrDefaultAsync(d => d.Id == id);
        }

        public async Task<List<Despesa>> GetDespesasByUserAsync(int userId)
        {
            return await _context.Despesas.Where(d => d.UsuarioId == userId).ToListAsync();
        }

        public async Task<List<Despesa>> GetDespesasByMonthAndUserAsync(int mes, int ano, int userId)
        {
            return await _context.Despesas
                .Where(d => d.Data.Month == mes && d.Data.Year == ano && d.UsuarioId == userId)
                .ToListAsync();
        }
    }
}