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
        {
            await EnsureCacheLoadedAsync();
        }

        return _cache!.TryGetValue(id, out var word) ? word : null;
    }
    
    private async Task EnsureCacheLoadedAsync()
    {
        if (_cache is not null) 
            return;
        
        await using var stream = await _httpClient.GetStreamAsync("data/data-v1.csv");
        using var reader = new StreamReader(stream);

        var words = new Dictionary<int, WordEntity>(DomainConsts.TotalWords);
        
        int currentIndex = 0;
        string? line;
        
        while ((line = await reader.ReadLineAsync()) != null)
        {
            if (string.IsNullOrWhiteSpace(line)) 
                continue;
            
            var separatorIndex = line.IndexOf(';');
            if (separatorIndex == -1) 
                continue;
            
            var value = line.Substring(0, separatorIndex).Trim();
            if (separatorIndex + 1 >= line.Length) 
                continue;

            var translation = line.Substring(separatorIndex + 1).Trim();
            
            currentIndex++;
            
            var entity = new WordEntity
            {
                Id = currentIndex,
                Value = value,
                Translation = translation
            };
            
            words[currentIndex] = entity;
        }
        
        _cache = words;
    }
}
