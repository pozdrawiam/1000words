using Otw.Core.Domain;

namespace Otw.Core.Application.Settings;

public interface ILearnSortTypeQueryHandler
{
    Task<WordSortType> ExecuteAsync();
}

public class LearnSortTypeQueryHandler : ILearnSortTypeQueryHandler
{
    public Task<WordSortType> ExecuteAsync()
    {
        throw new NotImplementedException();
    }
}
