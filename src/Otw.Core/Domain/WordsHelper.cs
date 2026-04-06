namespace Otw.Core.Domain;

public static class WordsHelper
{
    public static async Task<(double, int)> CalculateProgressPercentageAsync(
        IWordsRepository repo, WordSortType sortType, int currentWordId)
    {
        var words = (await GetWordsSortedAsync(repo, sortType)).ToList();
        var index = words.FindIndex(w => w.Id == currentWordId) + 1;
        var progress = (double)index / words.Count;
        var progressPercent = (int)((double)index / words.Count * 100);
        
        return (progress, progressPercent);
    }
    
    public static async Task<IEnumerable<WordEntity>> GetWordsSortedAsync(IWordsRepository repo, WordSortType sortType)
    {
        var words = await repo.GetAllAsync();

        return sortType switch
        {
            WordSortType.Default => words,
            WordSortType.AlphabeticalAsc => words.OrderBy(w => w.Value),
            WordSortType.AlphabeticalDesc => words.OrderByDescending(w => w.Value),
            WordSortType.Random => words.OrderBy(_ => Random.Shared.Next()),
            _ => throw new ArgumentOutOfRangeException(nameof(sortType), sortType, null)
        };
    }
}
