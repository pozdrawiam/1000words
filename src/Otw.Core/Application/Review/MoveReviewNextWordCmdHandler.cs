using Otw.Core.Domain;

namespace Otw.Core.Application.Review;

public interface IMoveReviewNextWordCmdHandler
{
    Task<WordEntity> ExecuteAsync(int currentWordId);
}

public class MoveReviewNextWordCmdHandler : IMoveReviewNextWordCmdHandler
{
    private readonly IParametersRepository _parameters;
    private readonly IWordsRepository _repo;

    public MoveReviewNextWordCmdHandler(IParametersRepository parameters, IWordsRepository repo)
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
            await _parameters.SetReviewLastWordIdAsync(nextWord.Id);
            
            return nextWord;
        }
        
        var firstWord = (await _repo.GetAllAsync()).First();
        await _parameters.SetReviewLastWordIdAsync(firstWord.Id);
        
        return firstWord;
    }
}
