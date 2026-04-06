namespace Otw.Core.Infrastructure;

public interface ILocalStorageService
{
    ValueTask SetItemAsync(string key, string value);
    ValueTask<string?> GetItemAsync(string key);
    ValueTask RemoveItemAsync(string key);
}
