using Moq;
using Otw.Core.Application.Settings;
using Otw.Core.Domain;

namespace Otw.Core.Tests.Application.Settings;

public class ResetReviewProgressCmdHandlerTests
{
    private readonly ResetReviewProgressCmdHandler _sut;
    private readonly Mock<IParametersRepository> _parametersMock = new();

    public ResetReviewProgressCmdHandlerTests()
    {
        _sut = new(_parametersMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldResetReviewProgress()
    {
        // Act
        await _sut.ExecuteAsync();

        // Assert
        _parametersMock.Verify(p => p.ResetReviewProgressAsync(), Times.Once);
    }
}
