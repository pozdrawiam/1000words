namespace Otw.Core.Domain;

public interface ILocalStorageService
{
    ValueTask SetItemAsync(string key, string value);
    ValueTask<string?> GetItemAsync(string key);
}
