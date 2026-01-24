using Moq;
using Otw.Core.Application.Settings;
using Otw.Core.Domain;

namespace Otw.Core.Tests.Application.Settings;

public class GetReviewSortTypeQueryHandlerTests
{
    private readonly GetReviewSortTypeQueryHandler _sut;
    private readonly Mock<IParametersRepository> _parametersMock = new();

    public GetReviewSortTypeQueryHandlerTests()
    {
        _sut = new(_parametersMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnSortTypeFromParameters()
    {
        // Arrange
        var expectedSortType = WordSortType.AlphabeticalAsc;
        _parametersMock.Setup(p => p.GetReviewSortTypeAsync())
            .ReturnsAsync(expectedSortType);

        // Act
        var result = await _sut.ExecuteAsync();

        // Assert
        Assert.Equal(expectedSortType, result);
        _parametersMock.Verify(p => p.GetReviewSortTypeAsync(), Times.Once);
    }
}
