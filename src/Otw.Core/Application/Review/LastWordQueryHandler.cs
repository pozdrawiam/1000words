using Otw.Core.Domain;

namespace Otw.Core.Application.Review;

public interface ILastWordQueryHandler
{
    Task<WordEntity> ExecuteAsync();
}

public class LastWordQueryHandler : ILastWordQueryHandler
{
    private readonly IParametersRepository _parameters;
    private readonly IWordsRepository _repo;

    public LastWordQueryHandler(IParametersRepository parameters, IWordsRepository repo)
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
