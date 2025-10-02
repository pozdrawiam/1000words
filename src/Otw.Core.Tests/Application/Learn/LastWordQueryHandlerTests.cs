using Moq;
using Otw.Core.Application.Learn;
using Otw.Core.Domain;

namespace Otw.Core.Tests.Application.Learn;

public class LastWordQueryHandlerTests
{
    private readonly LastWordQueryHandler _sut;
    
    private readonly Mock<IWordsRepository> _repoMock = new();

    public LastWordQueryHandlerTests()
    {
        _sut = new(_repoMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnWordWithId1_WhenItExists()
    {
        var expectedWord = new WordEntity
        {
            Id = 1,
            Value = "Test1",
            Translation = "Test1Translation"
        };
        
        _repoMock.Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(expectedWord);

        // Act
        var result = await _sut.ExecuteAsync();
        
        Assert.Equal(expectedWord, result);
        _repoMock.Verify(r => r.GetByIdAsync(1), Times.Once);
        _repoMock.Verify(r => r.GetAllAsync(), Times.Never);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnFirstWord_WhenWordWithId1DoesNotExist()
    {
        _repoMock.Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync((WordEntity?)null);

        var words = new WordEntity[]
        {
            new() { Id = 2, Value = "B", Translation = "B" },
            new() { Id = 3, Value = "C", Translation = "C" },
        };

        _repoMock.Setup(r => r.GetAllAsync())
            .ReturnsAsync(words);

        // Act
        var result = await _sut.ExecuteAsync();
        
        Assert.Equal(words.First(), result);
        _repoMock.Verify(r => r.GetByIdAsync(1), Times.Once);
        _repoMock.Verify(r => r.GetAllAsync(), Times.Once);
    }
}
