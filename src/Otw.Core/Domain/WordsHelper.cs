namespace Otw.Core.Domain;

public static class WordsHelper
{
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
