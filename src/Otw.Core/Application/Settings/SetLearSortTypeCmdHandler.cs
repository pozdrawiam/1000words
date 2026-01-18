using Otw.Core.Domain;

namespace Otw.Core.Application.Settings;

public interface ISetLearSortTypeCmdHandler
{
    Task ExecuteAsync(WordSortType sortType);
}

public class SetLearSortTypeCmdHandler : ISetLearSortTypeCmdHandler
{
    public Task ExecuteAsync(WordSortType sortType)
    {
        throw new NotImplementedException();
    }
}
