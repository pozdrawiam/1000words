using Otw.Core.Domain;

namespace Otw.Core.Application.Settings;

public interface IReviewSortTypeQueryHandler
{
    Task<WordSortType> ExecuteAsync();
}

public class ReviewSortTypeQueryHandler : IReviewSortTypeQueryHandler
{
    private const string Key = "Review_WordSortType";
    private readonly ILocalStorageService _localStorage;

    public ReviewSortTypeQueryHandler(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public async Task<WordSortType> ExecuteAsync()
    {
        var value = await _localStorage.GetItemAsync(Key);
        if (string.IsNullOrEmpty(value) || !Enum.TryParse<WordSortType>(value, out var result))
        {
            return WordSortType.Default;
        }

        return result;
    }
}
