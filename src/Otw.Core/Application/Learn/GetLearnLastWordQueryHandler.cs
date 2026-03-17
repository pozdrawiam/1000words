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
    
    public async Task<WordEntity> ExecuteAsync()
    {
        var lastWordId = await ResolveLastWordIdAsync();
        var lastWord = await _repo.GetByIdAsync(lastWordId);

        return lastWord ?? (await _repo.GetAllAsync()).First();
    }

    private async Task<int> ResolveLastWordIdAsync()
    {
        var storedId = await _parameters.GetLearnLastWordIdAsync();
        if (storedId.HasValue)
            return storedId.Value;

        var sortType = await _parameters.GetLearnSortTypeAsync();
        var words = await WordsHelper.GetWordsSortedAsync(_repo, sortType);

        return words.First().Id;
    }
}
