using Bunit;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Moq;
using Otw.Core.Application.Review;
using Otw.Core.Application.Settings;
using Otw.Core.Domain;
using Otw.WebApp.Pages;

namespace Otw.WebApp.Tests.Pages;

public class ReviewTests : BunitContext
{
    private readonly Mock<IStringLocalizer<Resources.Pages.Review>> _localizer = new();
    private readonly Mock<IGetReviewLastWordQueryHandler> _lastWordHandler = new();
    private readonly Mock<IGetReviewSortTypeQueryHandler> _sortTypeQueryHandler = new();
    private readonly Mock<IMoveReviewNextWordCmdHandler> _nextWordHandler = new();
    private readonly Mock<IMoveReviewPrevWordCmdHandler> _prevWordHandler = new();

    public ReviewTests()
    {
        Services.AddSingleton(_localizer.Object);
        Services.AddSingleton(_lastWordHandler.Object);
        Services.AddSingleton(_sortTypeQueryHandler.Object);
        Services.AddSingleton(_nextWordHandler.Object);
        Services.AddSingleton(_prevWordHandler.Object);
        
        _localizer.Setup(x => x[It.IsAny<string>()])
            .Returns((string key) => new LocalizedString(key, key));
    }

    [Fact]
    public void Should_ShowLoading_When_Loading()
    {
        // Arrange
        _lastWordHandler.Setup(x => x.ExecuteAsync())
            .Returns(async () =>
            {
                await Task.Delay(100);
                return new WordEntity { Id = 1, Value = "Cat", Translation = "Kot" };
            });

        // Act
        var cut = Render<Review>();

        // Assert
        Assert.Contains("Loading", cut.Markup); 
    }

    [Fact]
    public void Should_ShowWord_When_Loaded()
    {
        // Arrange
        var word = new WordEntity { Id = 1, Value = "Cat", Translation = "Kot" };
        _lastWordHandler.Setup(x => x.ExecuteAsync()).ReturnsAsync(word);

        // Act
        var cut = Render<Review>();

        // Assert
        Assert.Contains("Cat", cut.Markup);
        // Translation hidden initially
        var translationElement = cut.Find("p.card-text");
        Assert.Contains("visibility:hidden", translationElement.GetAttribute("style")?.Replace(" ", "")); 
    }

    [Fact]
    public void Should_ShowTranslation_When_ButtonClicked()
    {
        // Arrange
        var word = new WordEntity { Id = 1, Value = "Cat", Translation = "Kot" };
        _lastWordHandler.Setup(x => x.ExecuteAsync()).ReturnsAsync(word);

        var cut = Render<Review>();

        // Act
        cut.Find("button.btn-success").Click();
        
        // Assert
        var translationElement = cut.Find("p.card-text");
        Assert.Contains("visible", translationElement.GetAttribute("style"));
    }

    [Fact]
    public async Task Should_LoadNextWord()
    {
        // Arrange
        var word1 = new WordEntity { Id = 1, Value = "Cat", Translation = "Kot" };
        var word2 = new WordEntity { Id = 2, Value = "Dog", Translation = "Pies" };
        
        _lastWordHandler.Setup(x => x.ExecuteAsync()).ReturnsAsync(word1);
        _nextWordHandler.Setup(x => x.ExecuteAsync(word1.Id)).ReturnsAsync(word2);

        var cut = Render<Review>();
        
        // Act
        await cut.FindAll("button.btn-primary").First(b => b.TextContent.Contains("NextWord")).ClickAsync(new MouseEventArgs());

        // Assert
        Assert.Contains("Dog", cut.Markup);
    }
    
    [Fact]
    public async Task Should_LoadPreviousWord()
    {
        // Arrange
        var word2 = new WordEntity { Id = 2, Value = "Dog", Translation = "Pies" };
        var word1 = new WordEntity { Id = 1, Value = "Cat", Translation = "Kot" };
        
        _lastWordHandler.Setup(x => x.ExecuteAsync()).ReturnsAsync(word2);
        _prevWordHandler.Setup(x => x.ExecuteAsync(word2.Id)).ReturnsAsync(word1);

        var cut = Render<Review>();
        
        // Act
        await cut.FindAll("button.btn-secondary")
            .First(b => b.TextContent.Contains("PreviousWord"))
            .ClickAsync(new MouseEventArgs());

        // Assert
        Assert.Contains("Cat", cut.Markup);
    }
}
