using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Otw.Core.Application.Learn;
using Otw.Core.Domain;

namespace Otw.WebApp.Tests.Pages;

public class LearnTests : TestContext
{
    private readonly Mock<ILastWordQueryHandler> _lastWOrdQueryHandler = new();
    private readonly Mock<INextWordCmdHandler> _nextWordCmdHandler = new();
    
    public LearnTests()
    {
        Services.AddSingleton(_lastWOrdQueryHandler.Object);
        Services.AddSingleton(_nextWordCmdHandler.Object);
    }
    
    [Fact]
    public void Should_RenderWord()
    {
        _lastWOrdQueryHandler.Setup(x => x.ExecuteAsync())
            .ReturnsAsync(new WordEntity
            {
                Id = 1,
                Value = "Test1",
                Translation = "Test1Translation"
            });
        
        // Act
        var cut = RenderComponent<Otw.WebApp.Pages.Learn>();
        
        Assert.Contains("Test1", cut.Markup);
    }
}