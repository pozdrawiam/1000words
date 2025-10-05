using Microsoft.Extensions.DependencyInjection;
using Otw.Core.Domain;
using Otw.Core.Infrastructure;

namespace Otw.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddScoped<Application.Learn.ILastWordQueryHandler, Application.Learn.LastWordQueryHandler>();
        services.AddScoped<Application.Learn.INextWordCmdHandler, Application.Learn.NextWordCmdHandler>();
        
        services.AddScoped<Application.Review.ILastWordQueryHandler, Application.Review.LastWordQueryHandler>();
        services.AddScoped<Application.Review.INextWordCmdHandler, Application.Review.NextWordCmdHandler>();
        
        return services;
    }
    
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ILocalStorageService, LocalStorageService>();
        services.AddScoped<IWordsRepository, WordsRepository>();
        
        return services;
    }
}
