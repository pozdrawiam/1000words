using Otw.Core.Domain;

namespace Otw.Core.Application.Learn;

public interface IMoveLearnNextWordCmdHandler
{
    Task<WordEntity> ExecuteAsync(int currentWordId);
}

public sealed class MoveLearnNextWordCmdHandler : IMoveLearnNextWordCmdHandler
{
    private readonly IParametersRepository _parameters;
    private readonly IWordsRepository _repo;

    public MoveLearnNextWordCmdHandler(IParametersRepository parameters, IWordsRepository repo)
    {
        _parameters = parameters;
        _repo = repo;
    }

    public async Task<WordEntity> ExecuteAsync(int currentWordId)
    {
        var sortType = await _parameters.GetLearnSortTypeAsync();
        var nextWord = await GetNextWordAsync(currentWordId, sortType);
        
        await _parameters.SetLearnLastWordIdAsync(nextWord.Id);
            
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
