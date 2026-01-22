using Otw.Core.Domain;

namespace Otw.Core.Application.Review;

public interface IMoveReviewPrevWordCmdHandler
{
    Task<WordEntity> ExecuteAsync(int currentWordId);
}

public class MoveReviewPrevWordCmdHandler : IMoveReviewPrevWordCmdHandler
{
    private readonly IParametersRepository _parameters;
    private readonly IWordsRepository _repo;

    public MoveReviewPrevWordCmdHandler(IParametersRepository parameters, IWordsRepository repo)
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
            await _parameters.SetReviewLastWordIdAsync(previousWord.Id);
            
            return previousWord;
        }
        
        var firstWord = (await _repo.GetAllAsync()).First();
        await _parameters.SetReviewLastWordIdAsync(firstWord.Id);
        
        return firstWord;
    }
}
