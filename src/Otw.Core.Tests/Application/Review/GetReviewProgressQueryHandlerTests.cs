using Moq;
using Otw.Core.Application.Review;
using Otw.Core.Domain;

namespace Otw.Core.Tests.Application.Review;

public class GetReviewProgressQueryHandlerTests
{
    private readonly GetReviewProgressQueryHandler _sut;
    
    private readonly Mock<IParametersRepository> _parametersMock = new();
    private readonly Mock<IWordsRepository> _repoMock = new();

    public GetReviewProgressQueryHandlerTests()
    {
        _sut = new(_parametersMock.Object, _repoMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnCorrectProgress_WhenSortTypeIsDefault()
    {
        var words = new WordEntity[]
        {
            new() { Id = 1, Value = "A", Translation = "A" },
            new() { Id = 2, Value = "B", Translation = "B" },
            new() { Id = 3, Value = "C", Translation = "C" },
        };
        
        _parametersMock.Setup(p => p.GetReviewSortTypeAsync())
            .ReturnsAsync(WordSortType.Default);
        
        _repoMock.Setup(r => r.GetAllAsync())
            .ReturnsAsync(words);

        // Act
        var result = await _sut.ExecuteAsync(2);
        
        // For currentWordId=2, index=2 (1-based), count=3, progress=2/3≈0.666, percent=66
        Assert.Equal((2.0 / 3.0, 66), result);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnCorrectProgress_WhenSortTypeIsAlphabeticalAsc()
    {
        var words = new WordEntity[]
        {
            new() { Id = 3, Value = "C", Translation = "C" },
            new() { Id = 1, Value = "A", Translation = "A" },
            new() { Id = 2, Value = "B", Translation = "B" },
        };
        
        _parametersMock.Setup(p => p.GetReviewSortTypeAsync())
            .ReturnsAsync(WordSortType.AlphabeticalAsc);
        
        _repoMock.Setup(r => r.GetAllAsync())
            .ReturnsAsync(words);

        // Act
        var result = await _sut.ExecuteAsync(2);
        
        // Sorted: A(1), B(2), C(3), current=2 (B), index=2, progress=2/3≈0.666, percent=66
        Assert.Equal((2.0 / 3.0, 66), result);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnZeroProgress_WhenCurrentWordIdNotFound()
    {
        var words = new WordEntity[]
        {
            new() { Id = 1, Value = "A", Translation = "A" },
            new() { Id = 2, Value = "B", Translation = "B" },
        };
        
        _parametersMock.Setup(p => p.GetReviewSortTypeAsync())
            .ReturnsAsync(WordSortType.Default);
        
        _repoMock.Setup(r => r.GetAllAsync())
            .ReturnsAsync(words);

        // Act
        var result = await _sut.ExecuteAsync(999);
        
        // Not found, index=0+1=1? Wait, FindIndex returns -1, -1+1=0, progress=0/2=0, percent=0
        Assert.Equal((0.0, 0), result);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnFullProgress_WhenCurrentWordIsLast()
    {
        var words = new WordEntity[]
        {
            new() { Id = 1, Value = "A", Translation = "A" },
            new() { Id = 2, Value = "B", Translation = "B" },
        };
        
        _parametersMock.Setup(p => p.GetReviewSortTypeAsync())
            .ReturnsAsync(WordSortType.Default);
        
        _repoMock.Setup(r => r.GetAllAsync())
            .ReturnsAsync(words);

        // Act
        var result = await _sut.ExecuteAsync(2);
        
        // index=2, count=2, progress=1.0, percent=100
        Assert.Equal((1.0, 100), result);
    }
}
