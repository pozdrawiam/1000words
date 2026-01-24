using Moq;
using Otw.Core.Application.Settings;
using Otw.Core.Domain;

namespace Otw.Core.Tests.Application.Settings;

public class ResetLearnProgressCmdHandlerTests
{
    private readonly ResetLearnProgressCmdHandler _sut;
    private readonly Mock<IParametersRepository> _parametersMock = new();

    public ResetLearnProgressCmdHandlerTests()
    {
        _sut = new(_parametersMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldResetLearnProgress()
    {
        // Act
        await _sut.ExecuteAsync();

        // Assert
        _parametersMock.Verify(p => p.ResetLearnProgressAsync(), Times.Once);
    }
}
