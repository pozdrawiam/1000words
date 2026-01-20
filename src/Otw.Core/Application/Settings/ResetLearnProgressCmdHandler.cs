using Otw.Core.Domain;

namespace Otw.Core.Application.Settings;

public interface IResetLearnProgressCmdHandler
{
    Task ExecuteAsync();
}

public sealed class ResetLearnProgressCmdHandler : IResetLearnProgressCmdHandler
{
    private readonly ILocalStorageService _localStorage;

    public ResetLearnProgressCmdHandler(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public async Task ExecuteAsync()
    {
        await _localStorage.RemoveItemAsync("Learn_lastWordId");
    }
}
