using Otw.Core.Domain;

namespace Otw.Core.Application.Settings;

public interface IResetReviewProgressCmdHandler
{
    Task ExecuteAsync();
}

public sealed class ResetReviewProgressCmdHandler : IResetReviewProgressCmdHandler
{
    private readonly ILocalStorageService _localStorage;

    public ResetReviewProgressCmdHandler(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public async Task ExecuteAsync()
    {
        await _localStorage.RemoveItemAsync("Review_lastWordId");
    }
}
