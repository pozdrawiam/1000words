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
        int lastWordId = 1;
        var storedId = await _parameters.GetReviewLastWordIdAsync();
        
        if (storedId.HasValue)
            lastWordId = storedId.Value;

        var lastWord = await _repo.GetByIdAsync(lastWordId);

        if (lastWord is not null)
            return lastWord;
        
        var firstWord = (await _repo.GetAllAsync()).First();
        return firstWord;
    }
}
