using Otw.Core.Domain;

namespace Otw.Core.Application.Learn;

public interface IGetLearnProgressQueryHandler
{
    Task<(double, int)> ExecuteAsync(int currentWordId);
}

public class GetLearnProgressQueryHandler : IGetLearnProgressQueryHandler
{
    private readonly IParametersRepository _parameters;
    private readonly IWordsRepository _repo;

    public GetLearnProgressQueryHandler(IParametersRepository parameters, IWordsRepository repo)
    {
        _parameters = parameters;
        _repo = repo;
    }
    
    public async Task<(double, int)> ExecuteAsync(int currentWordId)
    {
        var result = await WordsHelper.CalculateProgressPercentageAsync(
            _repo, await _parameters.GetLearnSortTypeAsync(), currentWordId);
        
        return result;
    }
}
