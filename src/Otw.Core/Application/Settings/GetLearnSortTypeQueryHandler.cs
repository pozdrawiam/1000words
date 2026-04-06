using Otw.Core.Domain;

namespace Otw.Core.Application.Settings;

public interface IGetLearnSortTypeQueryHandler
{
    Task<WordSortType> ExecuteAsync();
}

public class GetLearnSortTypeQueryHandler : IGetLearnSortTypeQueryHandler
{
    private readonly IParametersRepository _parameters;

    public GetLearnSortTypeQueryHandler(IParametersRepository parameters)
    {
        _parameters = parameters;
    }

    public async Task<WordSortType> ExecuteAsync()
    {
        return await _parameters.GetLearnSortTypeAsync();
    }
}
