using Otw.Core.Domain;

namespace Otw.Core.Infrastructure;

public sealed class WordsRepository : IWordsRepository
{
    private readonly HttpClient _httpClient;
    
    private Dictionary<int, WordEntity>? _cache;

    public WordsRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<WordEntity[]> GetAllAsync()
    {
        if (_cache is not null)
            return _cache.Values.ToArray();

        await EnsureCacheLoadedAsync();
        
        return _cache!.Values.ToArray();
    }

    public async Task<WordEntity?> GetByIdAsync(int id)
    {
        if (_cache is null)
            await EnsureCacheLoadedAsync();

        return _cache!.GetValueOrDefault(id);
    }
    
    private async Task EnsureCacheLoadedAsync()
    {
        if (_cache is not null) 
            return;
        
        await using var stream = await _httpClient.GetStreamAsync("data/data-v1.csv");
        using var reader = new StreamReader(stream);

        var words = new Dictionary<int, WordEntity>(DomainConsts.TotalWords);
        var currentIndex = 0;

        while (await reader.ReadLineAsync() is { } line)
        {
            if (string.IsNullOrWhiteSpace(line)) 
                continue;
            
            var separatorIndex = line.IndexOf(';');
            if (separatorIndex == -1) 
                continue;
            
            var value = line[..separatorIndex].Trim();
            if (separatorIndex + 1 >= line.Length) 
                continue;

            var translation = line[(separatorIndex + 1)..].Trim();
            
            currentIndex++;
            
            words[currentIndex] = new WordEntity
            {
                Id = currentIndex,
                Value = value,
                Translation = translation
            };
        }
        
        _cache = words;
    }
}
