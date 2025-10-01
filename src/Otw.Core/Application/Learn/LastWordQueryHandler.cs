using Otw.Core.Domain;

namespace Otw.Core.Application.Learn;

public sealed class LastWordQueryHandler
{
    private readonly IWordsRepository _repo;

    public LastWordQueryHandler(IWordsRepository repo)
    {
        _repo = repo;
    }
    
    public async Task<WordEntity> ExecuteAsync()
    {
        const int lastWordId = 1;
        var lastWord = await _repo.GetByIdAsync(lastWordId);

        if (lastWord is not null)
            return lastWord;
        
        var firstWord = (await _repo.GetAllAsync()).First();
        return firstWord;
    }
}
