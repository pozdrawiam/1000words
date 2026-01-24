using Moq;
using Otw.Core.Application.Settings;
using Otw.Core.Domain;

namespace Otw.Core.Tests.Application.Settings;

public class SetLearSortTypeCmdHandlerTests
{
    private readonly SetLearSortTypeCmdHandler _sut;
    private readonly Mock<IParametersRepository> _parametersMock = new();

    public SetLearSortTypeCmdHandlerTests()
    {
        _sut = new(_parametersMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldSetSortTypeInParameters()
    {
        // Arrange
        var sortType = WordSortType.AlphabeticalAsc;

        // Act
        await _sut.ExecuteAsync(sortType);

        // Assert
        _parametersMock.Verify(p => p.SetLearnSortTypeAsync(sortType), Times.Once);
    }
}
