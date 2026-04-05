using Moq;
using Otw.Core.Application.Review;
using Otw.Core.Domain;

namespace Otw.Core.Tests.Application.Review;

public class MoveReviewPrevWordCmdHandlerTests
{
    private readonly MoveReviewPrevWordCmdHandler _sut;

    private readonly Mock<IParametersRepository> _parametersMock = new();
    private readonly Mock<IWordsRepository> _repoMock = new();
    
    public MoveReviewPrevWordCmdHandlerTests()
    {
        _sut = new(_parametersMock.Object, _repoMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnPreviousWord_WhenItExists()
    {
        var words = new WordEntity[]
        {
            new() { Id = 1, Value = "FirstWord", Translation = "" },
            new() { Id = 2, Value = "NextWord", Translation = "" }
        };

        _repoMock.Setup(r => r.GetAllAsync())
            .ReturnsAsync(words);

        _parametersMock.Setup(p => p.GetReviewLastWordIdAsync())
            .ReturnsAsync(2);
        
        _parametersMock.Setup(p => p.GetReviewSortTypeAsync())
            .ReturnsAsync(WordSortType.Default);
        
        // Act
        var result = await _sut.ExecuteAsync(2);
        
        Assert.Equal(words.First(), result);
        _repoMock.Verify(r => r.GetAllAsync(), Times.Once);

        _parametersMock.Verify(p => 
            p.SetReviewLastWordIdAsync(1), 
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
        _repoMock.Verify(r => r.GetAllAsync(), Times.Once);

        _parametersMock.Verify(p => 
            p.SetReviewLastWordIdAsync(words.First().Id), 
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
        
        _parametersMock.Verify(p => 
            p.SetReviewLastWordIdAsync(It.IsAny<int>()), 
            Times.Never);
    }
}
