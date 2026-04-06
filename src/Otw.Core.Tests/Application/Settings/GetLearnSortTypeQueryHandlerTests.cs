using Moq;
using Otw.Core.Application.Settings;
using Otw.Core.Domain;

namespace Otw.Core.Tests.Application.Settings;

public class GetLearnSortTypeQueryHandlerTests
{
    private readonly GetLearnSortTypeQueryHandler _sut;
    private readonly Mock<IParametersRepository> _parametersMock = new();

    public GetLearnSortTypeQueryHandlerTests()
    {
        _sut = new(_parametersMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnSortTypeFromParameters()
    {
        // Arrange
        var expectedSortType = WordSortType.Random;
        _parametersMock.Setup(p => p.GetLearnSortTypeAsync())
            .ReturnsAsync(expectedSortType);

        // Act
        var result = await _sut.ExecuteAsync();

        // Assert
        Assert.Equal(expectedSortType, result);
        _parametersMock.Verify(p => p.GetLearnSortTypeAsync(), Times.Once);
    }
}
