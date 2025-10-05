using Otw.Core.Domain;

namespace Otw.Core.Application.Learn;

public interface INextWordCmdHandler
{
    Task<WordEntity> ExecuteAsync(int currentWordId);
}

public sealed class NextWordCmdHandler : INextWordCmdHandler
{
    private readonly ILocalStorageService _localStorage;
    private readonly IWordsRepository _repo;

    public NextWordCmdHandler(ILocalStorageService localStorage, IWordsRepository repo)
    {
        _localStorage = localStorage;
        _repo = repo;
    }

    public async Task<WordEntity> ExecuteAsync(int currentWordId)
    {
        var nextWordId = currentWordId + 1;
        var nextWord = await _repo.GetByIdAsync(nextWordId);

        if (nextWord is not null)
        {
            await _localStorage.SetItemAsync("Learn_lastWordId", nextWord.Id.ToString());
            
            return nextWord;
        }
        
        var firstWord = (await _repo.GetAllAsync()).First();
        await _localStorage.SetItemAsync("Learn_lastWordId", firstWord.Id.ToString());
        
        return firstWord;
    }
}
