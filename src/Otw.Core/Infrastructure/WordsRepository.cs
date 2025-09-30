using Otw.Core.Domain;

namespace Otw.Core.Infrastructure;

public class WordsRepository : IWordsRepository
{
    private readonly HttpClient _httpClient;

    public WordsRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<WordEntity[]> GetAllAsync()
    {
        var csvContent = await _httpClient.GetStringAsync("data/data-v1.csv");
        
        var list = new List<(string English, string Polish)>();

        // ReSharper disable once UseCollectionExpression
        var lines = csvContent.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        
        for (int i = 0; i < lines.Length; i++)
        {
            var parts = lines[i].Split(';');
            if (parts.Length >= 2)
            {
                list.Add((parts[0].Trim(), parts[1].Trim()));
            }
        }
        
        return list.Select((x, index) => new WordEntity
        {
            Id = index + 1,
            Value = x.English,
            Translation = x.Polish
        }).ToArray();
    }
}
