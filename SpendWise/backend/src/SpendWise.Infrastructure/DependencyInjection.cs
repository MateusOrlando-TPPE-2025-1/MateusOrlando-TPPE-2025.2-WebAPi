using Microsoft.Extensions.DependencyInjection;
using SpendWise.Domain.Interfaces;
using SpendWise.Infrastructure.Repositories;
using SpendWise.Infrastructure.Services;
using SpendWise.Application.Services;

namespace SpendWise.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Add Repositories
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<ICategoriaRepository, CategoriaRepository>();
        services.AddScoped<ITransacaoRepository, TransacaoRepository>();
        services.AddScoped<IOrcamentoMensalRepository, OrcamentoMensalRepository>();
        services.AddScoped<IFechamentoMensalRepository, FechamentoMensalRepository>();
        services.AddScoped<IMetaRepository, MetaRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        // Add Authentication Services
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        
        // Add Email Service
        services.AddScoped<IEmailService, MockEmailService>();
        
        // Add Cache Service
        services.AddSingleton<ICacheService, InMemoryCacheService>();
        
        return services;
    }
}
