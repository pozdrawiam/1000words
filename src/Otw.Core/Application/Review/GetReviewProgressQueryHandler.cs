using Otw.Core.Domain;

namespace Otw.Core.Application.Review;

public interface IGetReviewProgressQueryHandler
{
    Task<(double, int)> ExecuteAsync(int currentWordId);
}

public class GetReviewProgressQueryHandler : IGetReviewProgressQueryHandler
{
    private readonly IParametersRepository _parameters;
    private readonly IWordsRepository _repo;

    public GetReviewProgressQueryHandler(IParametersRepository parameters, IWordsRepository repo)
    {
        _parameters = parameters;
        _repo = repo;
    }
    
    public async Task<(double, int)> ExecuteAsync(int currentWordId)
    {
        var result = await WordsHelper.CalculateProgressPercentageAsync(
            _repo, await _parameters.GetReviewSortTypeAsync(), currentWordId);
        
        return result;
    }
}
