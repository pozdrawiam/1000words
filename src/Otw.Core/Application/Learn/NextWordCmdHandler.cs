using Otw.Core.Domain;

namespace Otw.Core.Application.Learn;

public interface INextWordCmdHandler
{
    Task<WordEntity> ExecuteAsync(int currentWordId);
}

public sealed class NextWordCmdHandler : INextWordCmdHandler
{
    private readonly IParametersRepository _parameters;
    private readonly IWordsRepository _repo;

    public NextWordCmdHandler(IParametersRepository parameters, IWordsRepository repo)
    {
        _parameters = parameters;
        _repo = repo;
    }

    public async Task<WordEntity> ExecuteAsync(int currentWordId)
    {
        var nextWordId = currentWordId + 1;
        var nextWord = await _repo.GetByIdAsync(nextWordId);

        if (nextWord is not null)
        {
            await _parameters.SetLearnLastWordIdAsync(nextWord.Id);
            
            return nextWord;
        }
        
        var firstWord = (await _repo.GetAllAsync()).First();
        await _parameters.SetLearnLastWordIdAsync(firstWord.Id);
        
        return firstWord;
    }
}
