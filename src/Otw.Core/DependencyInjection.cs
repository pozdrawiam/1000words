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
        services.AddScoped<Application.Learn.IPreviousWordCmdHandler, Application.Learn.PreviousWordCmdHandler>();
        services.AddScoped<Application.Learn.IResetProgressCmdHandler, Application.Learn.ResetProgressCmdHandler>();
        
        services.AddScoped<Application.Review.ILastWordQueryHandler, Application.Review.LastWordQueryHandler>();
        services.AddScoped<Application.Review.INextWordCmdHandler, Application.Review.NextWordCmdHandler>();
        services.AddScoped<Application.Review.IPreviousWordCmdHandler, Application.Review.PreviousWordCmdHandler>();
        services.AddScoped<Application.Review.IResetProgressCmdHandler, Application.Review.ResetProgressCmdHandler>();
        
        services.AddScoped<Application.Settings.ILearnSortTypeQueryHandler, Application.Settings.LearnSortTypeQueryHandler>();
        services.AddScoped<Application.Settings.ISetLearSortTypeCmdHandler, Application.Settings.SetLearSortTypeCmdHandler>();
        services.AddScoped<Application.Settings.IReviewSortTypeQueryHandler, Application.Settings.ReviewSortTypeQueryHandler>();
        services.AddScoped<Application.Settings.ISetReviewSortTypeCmdHandler, Application.Settings.SetReviewSortTypeCmdHandler>();
        
        return services;
    }
    
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ILocalStorageService, LocalStorageService>();
        services.AddScoped<IWordsRepository, WordsRepository>();
        
        return services;
    }
}
