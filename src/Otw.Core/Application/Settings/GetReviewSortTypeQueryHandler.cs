using Otw.Core.Domain;

namespace Otw.Core.Application.Settings;

public interface IGetReviewSortTypeQueryHandler
{
    Task<WordSortType> ExecuteAsync();
}

public class GetReviewSortTypeQueryHandler : IGetReviewSortTypeQueryHandler
{
    private readonly IParametersRepository _parameters;

    public GetReviewSortTypeQueryHandler(IParametersRepository parameters)
    {
        _parameters = parameters;
    }

    public async Task<WordSortType> ExecuteAsync()
    {
        return await _parameters.GetReviewSortTypeAsync();
    }
}
