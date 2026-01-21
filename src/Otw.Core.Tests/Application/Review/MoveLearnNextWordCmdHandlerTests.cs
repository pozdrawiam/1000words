using Moq;
using Otw.Core.Application.Review;
using Otw.Core.Domain;

namespace Otw.Core.Tests.Application.Review;

public class MoveReviewNextWordCmdHandlerTests
{
    private readonly MoveReviewNextWordCmdHandler _sut;

    private readonly Mock<IParametersRepository> _parametersMock = new();
    private readonly Mock<IWordsRepository> _repoMock = new();
    
    public MoveReviewNextWordCmdHandlerTests()
    {
        _sut = new(_parametersMock.Object, _repoMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnNextWord_WhenItExists()
    {
        const int currentWordId = 5;
        var expectedNextWord = new WordEntity
        {
            Id = currentWordId + 1,
            Value = "NextWord",
            Translation = ""
        };

        _repoMock.Setup(r => r.GetByIdAsync(currentWordId + 1))
            .ReturnsAsync(expectedNextWord);

        // Act
        var result = await _sut.ExecuteAsync(currentWordId);
        
        Assert.Equal(expectedNextWord, result);
        _repoMock.Verify(r => r.GetByIdAsync(currentWordId + 1), Times.Once);
        _repoMock.Verify(r => r.GetAllAsync(), Times.Never);

        _parametersMock.Verify(p => 
            p.SetReviewLastWordIdAsync(expectedNextWord.Id), 
            Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnFirstWord_WhenNextWordDoesNotExist()
    {
        const int currentWordId = 10;

        _repoMock.Setup(r => r.GetByIdAsync(currentWordId + 1))
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
        _repoMock.Verify(r => r.GetByIdAsync(currentWordId + 1), Times.Once);
        _repoMock.Verify(r => r.GetAllAsync(), Times.Once);

        _parametersMock.Verify(p => 
            p.SetReviewLastWordIdAsync(words.First().Id), 
            Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldThrowInvalidOperationException_WhenNoWordsExist()
    {
        const int currentWordId = 3;

        _repoMock.Setup(r => r.GetByIdAsync(currentWordId + 1))
            .ReturnsAsync((WordEntity?)null);

        _repoMock.Setup(r => r.GetAllAsync())
            .ReturnsAsync([]);
        
        await Assert.ThrowsAsync<InvalidOperationException>(() => 
            // Act
            _sut.ExecuteAsync(currentWordId)
        );
        
        _parametersMock.Verify(p => 
            p.SetReviewLastWordIdAsync(It.IsAny<int>()), 
            Times.Never);
    }
}
