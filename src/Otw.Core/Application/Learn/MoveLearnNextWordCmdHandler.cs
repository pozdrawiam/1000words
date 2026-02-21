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
        var words = (await GetWordsSortedAsync(sortType)).ToList();
        var currentWordIndex = words.FindIndex(w => w.Id == currentWordId);

        if (currentWordIndex == -1)
            return words.First();
        
        var nextWordIndex = (currentWordIndex + 1) % words.Count;
        
        return nextWordIndex >= words.Count ? words.First() : words[nextWordIndex];
    }
    
    private async Task<IEnumerable<WordEntity>> GetWordsSortedAsync(WordSortType sortType)
    {
        var words = await _repo.GetAllAsync();

        return sortType switch
        {
            WordSortType.Default => words,
            WordSortType.AlphabeticalAsc => words.OrderBy(w => w.Value),
            WordSortType.AlphabeticalDesc => words.OrderByDescending(w => w.Value),
            WordSortType.Random => words,
            _ => throw new ArgumentOutOfRangeException(nameof(sortType), sortType, null)
        };
    }
}
