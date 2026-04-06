using Otw.Core.Domain;

namespace Otw.Core.Application.Review;

public interface IMoveReviewPrevWordCmdHandler
{
    Task<WordEntity> ExecuteAsync(int currentWordId);
}

public class MoveReviewPrevWordCmdHandler : IMoveReviewPrevWordCmdHandler
{
    private readonly IParametersRepository _parameters;
    private readonly IWordsRepository _repo;

    public MoveReviewPrevWordCmdHandler(IParametersRepository parameters, IWordsRepository repo)
    {
        _parameters = parameters;
        _repo = repo;
    }

    public async Task<WordEntity> ExecuteAsync(int currentWordId)
    {
        var sortType = await _parameters.GetReviewSortTypeAsync();
        var previousWord = await GetPreviousWordAsync(currentWordId, sortType);
        
        await _parameters.SetReviewLastWordIdAsync(previousWord.Id);
        
        return previousWord;
    }
    
    private async Task<WordEntity> GetPreviousWordAsync(int currentWordId, WordSortType sortType)
    {
        var words = (await WordsHelper.GetWordsSortedAsync(_repo, sortType)).ToList();
        var currentWordIndex = words.FindIndex(w => w.Id == currentWordId);

        if (currentWordIndex == -1)
            return words.First();
        
        var previousWordIndex = (currentWordIndex - 1) % words.Count;
        
        return previousWordIndex < 0 ? words.Last() : words[previousWordIndex];
    }
}
