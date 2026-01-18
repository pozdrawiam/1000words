using Otw.Core.Domain;

namespace Otw.Core.Application.Settings;

public interface ISetReviewSortTypeCmdHandler
{
    Task ExecuteAsync(WordSortType sortType);
}

public class SetReviewSortTypeCmdHandler : ISetReviewSortTypeCmdHandler
{
    private const string Key = "Review_WordSortType";
    private readonly ILocalStorageService _localStorage;

    public SetReviewSortTypeCmdHandler(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public async Task ExecuteAsync(WordSortType sortType)
    {
        await _localStorage.SetItemAsync(Key, sortType.ToString());
    }
}
