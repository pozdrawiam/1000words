using Otw.Core.Domain;

namespace Otw.Core.Infrastructure;

public class WordsRepository : IWordsRepository
{
    private readonly HttpClient _httpClient;
    
    private static WordEntity[]? _cache;

    public WordsRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<WordEntity[]> GetAllAsync()
    {
        if (_cache is not null)
            return _cache;

        _cache = await GetFromUrl();
        
        return _cache;
    }

    public async Task<WordEntity?> GetByIdAsync(int id)
    {
        var result = (await GetAllAsync()).FirstOrDefault(x => x.Id == id);
        return result;
    }
    
    private async Task<WordEntity[]> GetFromUrl()
    {
        var csvContent = await _httpClient.GetStringAsync("data/data-v1.csv");
        var words = new List<WordEntity>();

        // ReSharper disable once UseCollectionExpression
        var lines = csvContent.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        
        for (int i = 0; i < lines.Length; i++)
        {
            var parts = lines[i].Split(';');
            if (parts.Length >= 2)
            {
                words.Add(new()
                {
                    Id = i + 1,
                    Value = parts[0].Trim(),
                    Translation = parts[1].Trim()
                });
            }
        }
        
        return words.ToArray();
    }
}
