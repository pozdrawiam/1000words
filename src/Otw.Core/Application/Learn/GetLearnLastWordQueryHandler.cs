using Otw.Core.Domain;

namespace Otw.Core.Application.Learn;

public interface IGetLearnLastWordQueryHandler
{
    Task<WordEntity> ExecuteAsync();
}

public sealed class GetLearnLastWordQueryHandler : IGetLearnLastWordQueryHandler
{
    private readonly IParametersRepository _parameters;
    private readonly IWordsRepository _repo;

    public GetLearnLastWordQueryHandler(IParametersRepository parameters, IWordsRepository repo)
    {
        _parameters = parameters;
        _repo = repo;
    }
    
    public async Task<WordEntity> ExecuteAsync() //todo refactor
    {
        int lastWordId = 1;
        var storedId = await _parameters.GetLearnLastWordIdAsync();

        if (storedId.HasValue)
            lastWordId = storedId.Value;
        else
        {
            var sortType = await _parameters.GetLearnSortTypeAsync();
            var words = (await WordsHelper.GetWordsSortedAsync(_repo, sortType)).ToList();
            
            lastWordId = words.First().Id;
        }

        var lastWord = await _repo.GetByIdAsync(lastWordId);

        if (lastWord is not null)
            return lastWord;
        
        var firstWord = (await _repo.GetAllAsync()).First();
        return firstWord;
    }
}
