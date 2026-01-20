using Otw.Core.Domain;

namespace Otw.Core.Application.Settings;

public interface IResetLearnProgressCmdHandler
{
    Task ExecuteAsync();
}

public sealed class ResetLearnProgressCmdHandler : IResetLearnProgressCmdHandler
{
    private readonly IParametersRepository _parameters;

    public ResetLearnProgressCmdHandler(IParametersRepository parameters)
    {
        _parameters = parameters;
    }

    public async Task ExecuteAsync()
    {
        await _parameters.ResetLearnProgressAsync();
    }
}
