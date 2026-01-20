using Otw.Core.Domain;

namespace Otw.Core.Application.Settings;

public interface ILearnSortTypeQueryHandler
{
    Task<WordSortType> ExecuteAsync();
}

public class LearnSortTypeQueryHandler : ILearnSortTypeQueryHandler
{
    private readonly IParametersRepository _parameters;

    public LearnSortTypeQueryHandler(IParametersRepository parameters)
    {
        _parameters = parameters;
    }

    public async Task<WordSortType> ExecuteAsync()
    {
        return await _parameters.GetLearnSortTypeAsync();
    }
}
