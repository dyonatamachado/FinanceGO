using FinanceGO.Core.Repositories.DespesaRepositories;
using FinanceGO.Core.Repositories.ReceitaRepositories;
using FinanceGO.Core.Repositories.UsuarioRepositories;
using FinanceGO.Core.UserServices;
using FinanceGO.Infrastructure.Persistence.Repositories.DespesaRepositories;
using FinanceGO.Infrastructure.Persistence.Repositories.ReceitaRepositories;
using FinanceGO.Infrastructure.Persistence.Repositories.UsuarioRepositories;
using FinanceGO.Infrastructure.UserServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Configuration;
using System.Text;
using FinanceGO.Core.Authentication;
using FinanceGO.Infrastructure.Authentication;
using FinanceGO.Core.Authorization;
using FinanceGO.Infrastructure.Authorization;
using FinanceGO.Core.RulesValidators;
using FinanceGO.Application.Validators.RulesValidators;
using FinanceGO.Application.Validators.IRulesValidators;

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
            services.AddScoped<IUsuarioCommandRepository, UsuarioCommandRepository>();
            services.AddScoped<IUsuarioQueryRepository, UsuarioQueryRepository>();
        }

        public static void AddRulesValidators(this IServiceCollection services)
        {
            services.AddScoped<IEmailDuplicadoValidator, EmailDuplicadoValidator>();
            services.AddScoped<IDespesaDuplicadaValidator, DespesaDuplicadaValidator>();
            services.AddScoped<IReceitaDuplicadaValidator, ReceitaDuplicadaValidator>();
        }

        public static void AddAuthServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
        }

        public static void AddUserServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ILoggedUserService, LoggedUserService>();
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FinanceGO.API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                 {
                     {
                           new OpenApiSecurityScheme
                             {
                                 Reference = new OpenApiReference
                                 {
                                     Type = ReferenceType.SecurityScheme,
                                     Id = "Bearer"
                                 }
                             },
                             new string[] {}
                     }
                 });
            });
        }

        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services
              .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>
              {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidateLifetime = true,
                      ValidateIssuerSigningKey = true,

                      ValidIssuer = configuration["Jwt:Issuer"],
                      ValidAudience = configuration["Jwt:Audience"],
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                  };
              });
        }

        public static void ConfigureAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("HasUserId", policy => policy.RequireClaim("UserId"));
            });

            services.AddScoped<IMesmoUsuarioAuthorizationRequirement, MesmoUsuarioAuthorizationRequirement>();
        }
    }
}