using Moq;
using Otw.Core.Application.Settings;
using Otw.Core.Domain;

namespace Otw.Core.Tests.Application.Settings;

public class SetReviewSortTypeCmdHandlerTests
{
    private readonly SetReviewSortTypeCmdHandler _sut;
    private readonly Mock<IParametersRepository> _parametersMock = new();

    public SetReviewSortTypeCmdHandlerTests()
    {
        _sut = new(_parametersMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldSetSortTypeInParameters()
    {
        // Arrange
        var sortType = WordSortType.AlphabeticalDesc;

        // Act
        await _sut.ExecuteAsync(sortType);

        // Assert
        _parametersMock.Verify(p => p.SetReviewSortTypeAsync(sortType), Times.Once);
    }
}
