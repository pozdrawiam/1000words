using Otw.Core.Domain;

namespace Otw.Core.Application.Settings;

public interface IReviewSortTypeQueryHandler
{
    Task<WordSortType> ExecuteAsync();
}

public class ReviewSortTypeQueryHandler : IReviewSortTypeQueryHandler
{
    private readonly IParametersRepository _parameters;

    public ReviewSortTypeQueryHandler(IParametersRepository parameters)
    {
        _parameters = parameters;
    }

    public async Task<WordSortType> ExecuteAsync()
    {
        return await _parameters.GetReviewSortTypeAsync();
    }
}
