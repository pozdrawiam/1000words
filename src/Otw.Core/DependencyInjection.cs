using Microsoft.Extensions.DependencyInjection;
using Otw.Core.Application.Learn;
using Otw.Core.Application.Review;
using Otw.Core.Application.Settings;
using Otw.Core.Domain;
using Otw.Core.Infrastructure;

namespace Otw.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IGetLearnLastWordQueryHandler, GetLearnLastWordQueryHandler>();
        services.AddScoped<IGetLearnProgressQueryHandler, GetLearnProgressQueryHandler>();
        services.AddScoped<IMoveLearnNextWordCmdHandler, MoveLearnNextWordCmdHandler>();
        services.AddScoped<IMoveLearnPrevWordCmdHandler, MoveLearnPrevWordCmdHandler>();
        
        services.AddScoped<IGetReviewLastWordQueryHandler, GetReviewLastWordQueryHandler>();
        services.AddScoped<IMoveReviewNextWordCmdHandler, MoveReviewNextWordCmdHandler>();
        services.AddScoped<IMoveReviewPrevWordCmdHandler, MoveReviewPrevWordCmdHandler>();
        
        services.AddScoped<IGetLearnSortTypeQueryHandler, GetLearnSortTypeQueryHandler>();
        services.AddScoped<ISetLearSortTypeCmdHandler, SetLearSortTypeCmdHandler>();
        services.AddScoped<IResetLearnProgressCmdHandler, ResetLearnProgressCmdHandler>();
        services.AddScoped<IGetReviewSortTypeQueryHandler, GetReviewSortTypeQueryHandler>();
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
