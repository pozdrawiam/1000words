using Otw.Core.Domain;

namespace Otw.Core.Application.Settings;

public interface ISetReviewSortTypeCmdHandler
{
    Task ExecuteAsync(WordSortType sortType);
}

public class SetReviewSortTypeCmdHandler : ISetReviewSortTypeCmdHandler
{
    private readonly IParametersRepository _parameters;

    public SetReviewSortTypeCmdHandler(IParametersRepository parameters)
    {
        _parameters = parameters;
    }

    public async Task ExecuteAsync(WordSortType sortType)
    {
        await _parameters.SetReviewSortTypeAsync(sortType);
    }
}
