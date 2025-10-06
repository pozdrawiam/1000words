using Otw.Core.Domain;

namespace Otw.Core.Application.Review;

public interface INextWordCmdHandler
{
    Task<WordEntity> ExecuteAsync(int currentWordId);
}

public class NextWordCmdHandler : INextWordCmdHandler
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
            await _localStorage.SetItemAsync("Review_lastWordId", nextWord.Id.ToString());
            
            return nextWord;
        }
        
        var firstWord = (await _repo.GetAllAsync()).First();
        await _localStorage.SetItemAsync("Review_lastWordId", firstWord.Id.ToString());
        
        return firstWord;
    }
}
