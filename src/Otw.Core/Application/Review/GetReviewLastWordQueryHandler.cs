using Otw.Core.Domain;

namespace Otw.Core.Application.Review;

public interface IGetReviewLastWordQueryHandler
{
    Task<WordEntity> ExecuteAsync();
}

public class GetReviewLastWordQueryHandler : IGetReviewLastWordQueryHandler
{
    private readonly IParametersRepository _parameters;
    private readonly IWordsRepository _repo;

    public GetReviewLastWordQueryHandler(IParametersRepository parameters, IWordsRepository repo)
    {
        _parameters = parameters;
        _repo = repo;
    }
    
    public async Task<WordEntity> ExecuteAsync()
    {
        var lastWordId = await ResolveLastWordIdAsync();
        var lastWord = await _repo.GetByIdAsync(lastWordId);

        return lastWord ?? (await _repo.GetAllAsync()).First();
    }

    private async Task<int> ResolveLastWordIdAsync()
    {
        var storedId = await _parameters.GetReviewLastWordIdAsync();
        if (storedId.HasValue)
            return storedId.Value;

        var sortType = await _parameters.GetReviewSortTypeAsync();
        var words = await WordsHelper.GetWordsSortedAsync(_repo, sortType);

        return words.First().Id;
    }
}
