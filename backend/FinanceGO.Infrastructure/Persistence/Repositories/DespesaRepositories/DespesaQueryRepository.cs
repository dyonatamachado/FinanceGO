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

        public async Task<Despesa> ReadDespesaByMonth(int mes, int ano, string descricao)
        {
            var despesa = await _context.Despesas.SingleOrDefaultAsync(
                d => d.Data.Month == mes &&
                d.Data.Year == ano &&
                d.Descricao == descricao
            );

            return despesa;
        }
    }
}