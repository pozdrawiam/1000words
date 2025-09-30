using Microsoft.Extensions.DependencyInjection;
using Otw.Core.Domain;
using Otw.Core.Infrastructure;

namespace Otw.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IWordsRepository, WordsRepository>();
        
        return services;
    }
}
