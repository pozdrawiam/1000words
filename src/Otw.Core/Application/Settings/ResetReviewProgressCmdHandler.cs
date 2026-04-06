using Otw.Core.Domain;

namespace Otw.Core.Application.Settings;

public interface IResetReviewProgressCmdHandler
{
    Task ExecuteAsync();
}

public sealed class ResetReviewProgressCmdHandler : IResetReviewProgressCmdHandler
{
    private readonly IParametersRepository _parameters;

    public ResetReviewProgressCmdHandler(IParametersRepository parameters)
    {
        _parameters = parameters;
    }

    public async Task ExecuteAsync()
    {
        await _parameters.ResetReviewProgressAsync();
    }
}
