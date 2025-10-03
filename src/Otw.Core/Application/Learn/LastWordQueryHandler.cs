using Otw.Core.Domain;

namespace Otw.Core.Application.Learn;

public sealed class LastWordQueryHandler
{
    private readonly ILocalStorageService _localStorage;
    private readonly IWordsRepository _repo;

    public LastWordQueryHandler(ILocalStorageService localStorage, IWordsRepository repo)
    {
        _localStorage = localStorage;
        _repo = repo;
    }
    
    public async Task<WordEntity> ExecuteAsync()
    {
        int lastWordId = 1;
        var storedId = await _localStorage.GetItemAsync("Learn_lastWordId");
        
        if (!string.IsNullOrEmpty(storedId) && int.TryParse(storedId, out var parsedId))
            lastWordId = parsedId;

        var lastWord = await _repo.GetByIdAsync(lastWordId);

        if (lastWord is not null)
            return lastWord;
        
        var firstWord = (await _repo.GetAllAsync()).First();
        return firstWord;
    }
}
