using Moq;
using Otw.Core.Application.Review;
using Otw.Core.Domain;

namespace Otw.Core.Tests.Application.Review;

public class GetReviewLastWordQueryHandlerTests
{
    private readonly GetReviewLastWordQueryHandler _sut;
    
    private readonly Mock<IParametersRepository> _parametersMock = new();
    private readonly Mock<IWordsRepository> _repoMock = new();

    public GetReviewLastWordQueryHandlerTests()
    {
        _sut = new(_parametersMock.Object, _repoMock.Object);
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
    
    [Fact]
    public async Task ExecuteAsync_ShouldReturnWordWithIdFromLocalStorage_WhenItExists()
    {
        var expectedWord = new WordEntity
        {
            Id = 5,
            Value = "Five",
            Translation = ""
        };

        _parametersMock
            .Setup(p => p.GetReviewLastWordIdAsync())
            .ReturnsAsync(5);

        _repoMock.Setup(r => r.GetByIdAsync(5))
            .ReturnsAsync(expectedWord);

        // Act
        var result = await _sut.ExecuteAsync();
        
        Assert.Equal(expectedWord, result);
        _repoMock.Verify(r => r.GetByIdAsync(5), Times.Once);
        _repoMock.Verify(r => r.GetAllAsync(), Times.Never);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldFallbackTo1_WhenLocalStorageValueIsNull()
    {
        var expectedWord = new WordEntity
        {
            Id = 1,
            Value = "Fallback",
            Translation = ""
        };

        _parametersMock
            .Setup(p => p.GetReviewLastWordIdAsync())
            .ReturnsAsync((int?)null);

        _repoMock.Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(expectedWord);

        // Act
        var result = await _sut.ExecuteAsync();
        
        Assert.Equal(expectedWord, result);
        _repoMock.Verify(r => r.GetByIdAsync(1), Times.Once);
    }
}
