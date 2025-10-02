using Microsoft.JSInterop;
using Otw.Core.Domain;

namespace Otw.Core.Infrastructure;

public class LocalStorageService : ILocalStorageService
{
    private readonly IJSRuntime _jsRuntime;

    public LocalStorageService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public ValueTask SetItemAsync(string key, string value) =>
        _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, value);

    public ValueTask<string?> GetItemAsync(string key) =>
        _jsRuntime.InvokeAsync<string?>("localStorage.getItem", key);
}
