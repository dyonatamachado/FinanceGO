using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceGO.Core.Repositories.DespesaRepositories;
using FinanceGO.Core.Repositories.ReceitaRepositories;
using FinanceGO.Infrastructure.Persistence.Repositories.DespesaRepositories;
using FinanceGO.Infrastructure.Persistence.Repositories.ReceitaRepositories;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceGO.API.ServiceExtensions
{
    public static class ServiceExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IDespesaCommandRepository, DespesaCommandRepository>();
            services.AddScoped<IDespesaQueryRepository, DespesaQueryRepository>();
            services.AddScoped<IReceitaCommandRepository, ReceitaCommandRepository>();
            services.AddScoped<IReceitaQueryRepository, ReceitaQueryRepository>();
        }
    }
}