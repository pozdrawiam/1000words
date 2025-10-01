using Otw.Core.Domain;

namespace Otw.Core.Application.Learn;

public sealed class NextWordCmdHandler
{
    private readonly IWordsRepository _repo;

    public NextWordCmdHandler(IWordsRepository repo)
    {
        _repo = repo;
    }

    public async Task<WordEntity> ExecuteAsync(int currentWordId)
    {
        var nextWordId = currentWordId + 1;
        var nextWord = await _repo.GetByIdAsync(nextWordId);
        
        if (nextWord is not null)
            return nextWord;
        
        var firstWord = (await _repo.GetAllAsync()).First();
        return firstWord;
    }
}
