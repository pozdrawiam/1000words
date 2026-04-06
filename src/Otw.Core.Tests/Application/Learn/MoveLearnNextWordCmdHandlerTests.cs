using Moq;
using Otw.Core.Application.Learn;
using Otw.Core.Domain;

namespace Otw.Core.Tests.Application.Learn;

public class MoveLearnNextWordCmdHandlerTests
{
    private readonly MoveLearnNextWordCmdHandler _sut;

    private readonly Mock<IParametersRepository> _parametersMock = new();
    private readonly Mock<IWordsRepository> _repoMock = new();
    
    public MoveLearnNextWordCmdHandlerTests()
    {
        _sut = new(_parametersMock.Object, _repoMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnNextWord_WhenItExists()
    {
        var words = new WordEntity[]
        {
            new() { Id = 1, Value = "FirstWord", Translation = "" },
            new() { Id = 2, Value = "NextWord", Translation = "" }
        };

        _repoMock.Setup(r => r.GetAllAsync())
            .ReturnsAsync(words);

        _parametersMock.Setup(p => p.GetLearnLastWordIdAsync())
            .ReturnsAsync(1);
        
        _parametersMock.Setup(p => p.GetLearnSortTypeAsync())
            .ReturnsAsync(WordSortType.Default);
        
        // Act
        var result = await _sut.ExecuteAsync(1);
        
        Assert.Equal(words.Skip(1).First(), result);
        _repoMock.Verify(r => r.GetAllAsync(), Times.Once);

        _parametersMock.Verify(p => 
            p.SetLearnLastWordIdAsync(2), 
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
        _repoMock.Verify(r => r.GetAllAsync(), Times.Once);

        _parametersMock.Verify(p => 
            p.SetLearnLastWordIdAsync(words.First().Id), 
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
            p.SetLearnLastWordIdAsync(It.IsAny<int>()), 
            Times.Never);
    }
}
