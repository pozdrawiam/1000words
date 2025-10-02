using Microsoft.Extensions.DependencyInjection;
using Otw.Core.Application.Learn;
using Otw.Core.Domain;
using Otw.Core.Infrastructure;

namespace Otw.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddScoped<LastWordQueryHandler>();
        services.AddScoped<NextWordCmdHandler>();
        
        return services;
    }
    
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ILocalStorageService, LocalStorageService>();
        services.AddScoped<IWordsRepository, WordsRepository>();
        
        return services;
    }
}
