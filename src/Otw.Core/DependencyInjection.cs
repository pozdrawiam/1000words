using Microsoft.Extensions.DependencyInjection;
using Otw.Core.Application.Settings;
using Otw.Core.Domain;
using Otw.Core.Infrastructure;

namespace Otw.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<Application.Learn.ILastWordQueryHandler, Application.Learn.LastWordQueryHandler>();
        services.AddScoped<Application.Learn.INextWordCmdHandler, Application.Learn.NextWordCmdHandler>();
        services.AddScoped<Application.Learn.IPreviousWordCmdHandler, Application.Learn.PreviousWordCmdHandler>();
        
        services.AddScoped<Application.Review.ILastWordQueryHandler, Application.Review.LastWordQueryHandler>();
        services.AddScoped<Application.Review.INextWordCmdHandler, Application.Review.NextWordCmdHandler>();
        services.AddScoped<Application.Review.IPreviousWordCmdHandler, Application.Review.PreviousWordCmdHandler>();
        
        services.AddScoped<ILearnSortTypeQueryHandler, LearnSortTypeQueryHandler>();
        services.AddScoped<ISetLearSortTypeCmdHandler, SetLearSortTypeCmdHandler>();
        services.AddScoped<IResetLearnProgressCmdHandler, ResetLearnProgressCmdHandler>();
        services.AddScoped<IReviewSortTypeQueryHandler, ReviewSortTypeQueryHandler>();
        services.AddScoped<ISetReviewSortTypeCmdHandler, SetReviewSortTypeCmdHandler>();
        services.AddScoped<IResetReviewProgressCmdHandler, ResetReviewProgressCmdHandler>();
        
        return services;
    }
    
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ILocalStorageService, LocalStorageService>();
        services.AddScoped<IWordsRepository, WordsRepository>();
        
        return services;
    }
}
