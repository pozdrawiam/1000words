using Moq;
using Otw.Core.Application.Learn;
using Otw.Core.Domain;

namespace Otw.Core.Tests.Application.Learn;

public class PreviousWordCmdHandlerTests
{
    private readonly PreviousWordCmdHandler _sut;

    private readonly Mock<ILocalStorageService> _localStorageMock = new();
    private readonly Mock<IWordsRepository> _repoMock = new();
    
    public PreviousWordCmdHandlerTests()
    {
        _sut = new(_localStorageMock.Object, _repoMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnPreviousWord_WhenItExists()
    {
        const int currentWordId = 5;
        var expectedPreviousWord = new WordEntity
        {
            Id = currentWordId - 1,
            Value = "PreviousWord",
            Translation = ""
        };

        _repoMock.Setup(r => r.GetByIdAsync(currentWordId - 1))
            .ReturnsAsync(expectedPreviousWord);

        // Act
        var result = await _sut.ExecuteAsync(currentWordId);
        
        Assert.Equal(expectedPreviousWord, result);
        _repoMock.Verify(r => r.GetByIdAsync(currentWordId - 1), Times.Once);
        _repoMock.Verify(r => r.GetAllAsync(), Times.Never);

        _localStorageMock.Verify(ls => 
            ls.SetItemAsync("Learn_lastWordId", expectedPreviousWord.Id.ToString()), 
            Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnFirstWord_WhenPreviousWordDoesNotExist()
    {
        const int currentWordId = 10;

        _repoMock.Setup(r => r.GetByIdAsync(currentWordId - 1))
            .ReturnsAsync((WordEntity?)null);

        var words = new WordEntity[]
        {
            new() { Id = 1, Value = "FirstWord", Translation = "" },
            new() { Id = 2, Value = "AnotherWord", Translation = "" }
        };

        _repoMock.Setup(r => r.GetAllAsync())
            .ReturnsAsync(words);

        // Act
        var result = await _sut.ExecuteAsync(currentWordId);
        
        Assert.Equal(words.First(), result);
        _repoMock.Verify(r => r.GetByIdAsync(currentWordId - 1), Times.Once);
        _repoMock.Verify(r => r.GetAllAsync(), Times.Once);

        _localStorageMock.Verify(ls => 
            ls.SetItemAsync("Learn_lastWordId", words.First().Id.ToString()), 
            Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldThrowInvalidOperationException_WhenNoWordsExist()
    {
        const int currentWordId = 3;

        _repoMock.Setup(r => r.GetByIdAsync(currentWordId - 1))
            .ReturnsAsync((WordEntity?)null);

        _repoMock.Setup(r => r.GetAllAsync())
            .ReturnsAsync([]);
        
        await Assert.ThrowsAsync<InvalidOperationException>(() => 
            // Act
            _sut.ExecuteAsync(currentWordId)
        );
        
        _localStorageMock.Verify(ls => 
            ls.SetItemAsync(It.IsAny<string>(), It.IsAny<string>()), 
            Times.Never);
    }
}
