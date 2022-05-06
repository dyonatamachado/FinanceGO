using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceGO.Core.Repositories.DespesaRepositories;
using FinanceGO.Infrastructure.Persistence.Repositories.DespesaRepositories;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceGO.API.ServiceExtensions
{
    public static class ServiceExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IDespesaCommandRepository, DespesaCommandRepository>();
            services.AddScoped<IDespesaQueryRepository, DespesaQueryRepository>();
        }
    }
}