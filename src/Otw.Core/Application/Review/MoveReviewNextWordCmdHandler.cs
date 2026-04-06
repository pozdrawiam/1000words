using Otw.Core.Domain;

namespace Otw.Core.Application.Review;

public interface IMoveReviewNextWordCmdHandler
{
    Task<WordEntity> ExecuteAsync(int currentWordId);
}

public class MoveReviewNextWordCmdHandler : IMoveReviewNextWordCmdHandler
{
    private readonly IParametersRepository _parameters;
    private readonly IWordsRepository _repo;

    public MoveReviewNextWordCmdHandler(IParametersRepository parameters, IWordsRepository repo)
    {
        _parameters = parameters;
        _repo = repo;
    }

    public async Task<WordEntity> ExecuteAsync(int currentWordId)
    {
        var sortType = await _parameters.GetReviewSortTypeAsync();
        var nextWord = await GetNextWordAsync(currentWordId, sortType);
        
        await _parameters.SetReviewLastWordIdAsync(nextWord.Id);
            
        return nextWord;
    }
    
    private async Task<WordEntity> GetNextWordAsync(int currentWordId, WordSortType sortType)
    {
        var words = (await WordsHelper.GetWordsSortedAsync(_repo, sortType)).ToList();
        var currentWordIndex = words.FindIndex(w => w.Id == currentWordId);

        if (currentWordIndex == -1)
            return words.First();
        
        var nextWordIndex = (currentWordIndex + 1) % words.Count;
        
        return nextWordIndex >= words.Count ? words.First() : words[nextWordIndex];
    }
}
