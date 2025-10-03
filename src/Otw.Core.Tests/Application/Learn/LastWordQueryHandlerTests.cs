using Moq;
using Otw.Core.Application.Learn;
using Otw.Core.Domain;

namespace Otw.Core.Tests.Application.Learn;

public class LastWordQueryHandlerTests
{
    private readonly LastWordQueryHandler _sut;
    
    private readonly Mock<ILocalStorageService> _localStorageMock = new();
    private readonly Mock<IWordsRepository> _repoMock = new();

    public LastWordQueryHandlerTests()
    {
        _sut = new(_localStorageMock.Object, _repoMock.Object);
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

        _localStorageMock
            .Setup(ls => ls.GetItemAsync("Learn_lastWordId"))
            .ReturnsAsync("5");

        _repoMock.Setup(r => r.GetByIdAsync(5))
            .ReturnsAsync(expectedWord);

        // Act
        var result = await _sut.ExecuteAsync();
        
        Assert.Equal(expectedWord, result);
        _repoMock.Verify(r => r.GetByIdAsync(5), Times.Once);
        _repoMock.Verify(r => r.GetAllAsync(), Times.Never);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldFallbackTo1_WhenLocalStorageValueIsNotParsable()
    {
        var expectedWord = new WordEntity
        {
            Id = 1,
            Value = "Fallback",
            Translation = ""
        };

        _localStorageMock
            .Setup(ls => ls.GetItemAsync("Learn_lastWordId"))
            .ReturnsAsync("not-an-int");

        _repoMock.Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(expectedWord);

        // Act
        var result = await _sut.ExecuteAsync();
        
        Assert.Equal(expectedWord, result);
        _repoMock.Verify(r => r.GetByIdAsync(1), Times.Once);
    }
}
