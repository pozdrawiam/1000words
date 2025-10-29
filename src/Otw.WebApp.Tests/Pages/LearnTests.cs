using Bunit;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Otw.Core.Application.Learn;
using Otw.Core.Domain;

namespace Otw.WebApp.Tests.Pages;

public class LearnTests : TestContext
{
    private readonly Mock<ILastWordQueryHandler> _lastWordQueryHandler = new();
    private readonly Mock<INextWordCmdHandler> _nextWordCmdHandler = new();
    
    public LearnTests()
    {
        Services.AddSingleton(_lastWordQueryHandler.Object);
        Services.AddSingleton(_nextWordCmdHandler.Object);
    }
    
    [Fact]
    public void Should_RenderCurrentWord()
    {
        _lastWordQueryHandler.Setup(x => x.ExecuteAsync())
            .ReturnsAsync(new WordEntity
            {
                Id = 1,
                Value = "Test1",
                Translation = "Test1Translation"
            });
        
        // Act
        var cut = RenderComponent<Otw.WebApp.Pages.Learn>();
        
        Assert.Contains("Test1", cut.Markup);
        Assert.Contains("Test1Translation", cut.Markup);
    }

    [Fact]
    public async Task Should_RenderNextWord()
    {
        var firstWord = new WordEntity
        {
            Id = 1,
            Value = "Test1",
            Translation = "Test1Translation"
        };
        var nextWord = new WordEntity
        {
            Id = 2,
            Value = "Test2",
            Translation = "Test2Translation"
        };

        _lastWordQueryHandler
            .Setup(x => x.ExecuteAsync())
            .ReturnsAsync(firstWord);

        _nextWordCmdHandler
            .Setup(x => x.ExecuteAsync(firstWord.Id))
            .ReturnsAsync(nextWord);

        var cut = RenderComponent<Otw.WebApp.Pages.Learn>();
        var button = cut.Find("button.btn-primary");
        
        // Act
        await button.ClickAsync(new MouseEventArgs());
        
        Assert.Contains("Test2", cut.Markup);
        Assert.Contains("Test2Translation", cut.Markup);
    }
}
