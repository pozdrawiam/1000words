using Otw.Core.Domain;

namespace Otw.Core.Application.Learn;

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
        await _localStorage.RemoveItemAsync("Learn_lastWordId");
    }
}
