using Otw.Core.Domain;

namespace Otw.Core.Application.Learn;

public interface IPreviousWordCmdHandler
{
    Task<WordEntity> ExecuteAsync(int currentWordId);
}

public sealed class PreviousWordCmdHandler : IPreviousWordCmdHandler
{
    private readonly ILocalStorageService _localStorage;
    private readonly IWordsRepository _repo;

    public PreviousWordCmdHandler(ILocalStorageService localStorage, IWordsRepository repo)
    {
        _localStorage = localStorage;
        _repo = repo;
    }

    public async Task<WordEntity> ExecuteAsync(int currentWordId)
    {
        var previousWordId = currentWordId - 1;
        var previousWord = await _repo.GetByIdAsync(previousWordId);

        if (previousWord is not null)
        {
            await _localStorage.SetItemAsync("Learn_lastWordId", previousWord.Id.ToString());
            
            return previousWord;
        }
        
        var firstWord = (await _repo.GetAllAsync()).First();
        await _localStorage.SetItemAsync("Learn_lastWordId", firstWord.Id.ToString());
        
        return firstWord;
    }
}
