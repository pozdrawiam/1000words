using Otw.Core.Domain;

namespace Otw.Core.Application.Settings;

public interface ISetLearSortTypeCmdHandler
{
    Task ExecuteAsync(WordSortType sortType);
}

public class SetLearSortTypeCmdHandler : ISetLearSortTypeCmdHandler
{
    private readonly IParametersRepository _parameters;

    public SetLearSortTypeCmdHandler(IParametersRepository parameters)
    {
        _parameters = parameters;
    }

    public async Task ExecuteAsync(WordSortType sortType)
    {
        await _parameters.SetLearnSortTypeAsync(sortType);
    }
}
