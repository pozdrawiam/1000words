using Microsoft.Extensions.DependencyInjection;
using Otw.Core.Application.Settings;
using Otw.Core.Domain;
using Otw.Core.Infrastructure;

namespace Otw.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<Application.Learn.IGetLearnLastWordQueryHandler, Application.Learn.GetLearnLastWordQueryHandler>();
        services.AddScoped<Application.Learn.IMoveLearnNextWordCmdHandler, Application.Learn.MoveLearnNextWordCmdHandler>();
        services.AddScoped<Application.Learn.IMoveLearnPrevWordCmdHandler, Application.Learn.MoveLearnPrevWordCmdHandler>();
        
        services.AddScoped<Application.Review.IGetReviewLastWordQueryHandler, Application.Review.GetReviewLastWordQueryHandler>();
        services.AddScoped<Application.Review.IMoveReviewNextWordCmdHandler, Application.Review.MoveReviewNextWordCmdHandler>();
        services.AddScoped<Application.Review.IMoveReviewPrevWordCmdHandler, Application.Review.MoveReviewPrevWordCmdHandler>();
        
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
        services.AddScoped<IParametersRepository, ParametersRepository>();
        services.AddScoped<IWordsRepository, WordsRepository>();
        
        return services;
    }
}
