using Otw.Core.Domain;

namespace Otw.Core.Application.Learn;

public interface IPreviousWordCmdHandler
{
    Task<WordEntity> ExecuteAsync(int currentWordId);
}

public sealed class PreviousWordCmdHandler : IPreviousWordCmdHandler
{
    private readonly IParametersRepository _parameters;
    private readonly IWordsRepository _repo;

    public PreviousWordCmdHandler(IParametersRepository parameters, IWordsRepository repo)
    {
        _parameters = parameters;
        _repo = repo;
    }

    public async Task<WordEntity> ExecuteAsync(int currentWordId)
    {
        var previousWordId = currentWordId - 1;
        var previousWord = await _repo.GetByIdAsync(previousWordId);

        if (previousWord is not null)
        {
            await _parameters.SetLearnLastWordIdAsync(previousWord.Id);
            
            return previousWord;
        }
        
        var firstWord = (await _repo.GetAllAsync()).First();
        await _parameters.SetLearnLastWordIdAsync(firstWord.Id);
        
        return firstWord;
    }
}
