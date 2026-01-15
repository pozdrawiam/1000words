using Otw.Core.Domain;

namespace Otw.Core.Application.Review;

public interface IResetProgressCmdHandler
{
    Task ExecuteAsync();
}

public sealed class ResetProgressCmdHandler : IResetProgressCmdHandler
{
    private readonly ILocalStorageService _localStorage;

    public ResetProgressCmdHandler(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public async Task ExecuteAsync()
    {
        await _localStorage.RemoveItemAsync("Review_lastWordId");
    }
}
