using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FinanceGO.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinanceGO.Infrastructure.Persistence
{
    public class FinanceGODbContext : DbContext
    {
        public FinanceGODbContext(DbContextOptions<FinanceGODbContext> options) : base(options)
        {
            
        }

        public DbSet<Despesa> Despesas { get; set; }
        public DbSet<Receita> Receitas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        
    }
}