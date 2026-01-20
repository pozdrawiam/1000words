using Otw.Core.Domain;

namespace Otw.Core.Infrastructure;

public class ParametersRepository : IParametersRepository
{
    private readonly ILocalStorageService _localStorage;

    private const string LearnLastWordIdKey = "Learn_lastWordId";
    private const string ReviewLastWordIdKey = "Review_lastWordId";
    private const string LearnWordSortTypeKey = "Learn_WordSortType";
    private const string ReviewWordSortTypeKey = "Review_WordSortType";

    public ParametersRepository(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public async Task<int?> GetLearnLastWordIdAsync()
    {
        var value = await _localStorage.GetItemAsync(LearnLastWordIdKey);
        if (string.IsNullOrEmpty(value) || !int.TryParse(value, out var result))
        {
            return null;
        }
        return result;
    }

    public async Task SetLearnLastWordIdAsync(int id)
    {
        await _localStorage.SetItemAsync(LearnLastWordIdKey, id.ToString());
    }

    public async Task<int?> GetReviewLastWordIdAsync()
    {
        var value = await _localStorage.GetItemAsync(ReviewLastWordIdKey);
        if (string.IsNullOrEmpty(value) || !int.TryParse(value, out var result))
        {
            return null;
        }
        return result;
    }

    public async Task SetReviewLastWordIdAsync(int id)
    {
        await _localStorage.SetItemAsync(ReviewLastWordIdKey, id.ToString());
    }

    public async Task<WordSortType> GetLearnSortTypeAsync()
    {
        return await GetSortTypeAsync(LearnWordSortTypeKey);
    }

    public async Task SetLearnSortTypeAsync(WordSortType sortType)
    {
        await _localStorage.SetItemAsync(LearnWordSortTypeKey, sortType.ToString());
    }

    public async Task<WordSortType> GetReviewSortTypeAsync()
    {
        return await GetSortTypeAsync(ReviewWordSortTypeKey);
    }

    public async Task SetReviewSortTypeAsync(WordSortType sortType)
    {
        await _localStorage.SetItemAsync(ReviewWordSortTypeKey, sortType.ToString());
    }

    private async Task<WordSortType> GetSortTypeAsync(string key)
    {
        var value = await _localStorage.GetItemAsync(key);
        if (string.IsNullOrEmpty(value) || !Enum.TryParse<WordSortType>(value, out var result))
        {
            return WordSortType.Default;
        }
        return result;
    }

    public async Task ResetLearnProgressAsync()
    {
        await _localStorage.RemoveItemAsync(LearnLastWordIdKey);
    }

    public async Task ResetReviewProgressAsync()
    {
        await _localStorage.RemoveItemAsync(ReviewLastWordIdKey);
    }

    public async Task<bool> IsLearnStartedAsync()
    {
        var value = await _localStorage.GetItemAsync(Application.ApplicationConsts.LearnStartedKey);
        return value == Application.ApplicationConsts.LearnStartedValue;
    }

    public async Task SetLearnStartedAsync()
    {
        await _localStorage.SetItemAsync(Application.ApplicationConsts.LearnStartedKey, Application.ApplicationConsts.LearnStartedValue);
    }
}
