using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Moq;
using Otw.Core.Application.Settings;
using Otw.Core.Domain;
using Otw.WebApp.Pages;

namespace Otw.WebApp.Tests.Pages;

public class SettingsTests : BunitContext
{
    private readonly Mock<IStringLocalizer<Resources.Pages.Settings>> _localizer = new();
    private readonly Mock<IResetLearnProgressCmdHandler> _learnResetHandler = new();
    private readonly Mock<IResetReviewProgressCmdHandler> _reviewResetHandler = new();
    private readonly Mock<IGetLearnSortTypeQueryHandler> _learnSortHandler = new();
    private readonly Mock<ISetLearSortTypeCmdHandler> _setLearnSortHandler = new();
    private readonly Mock<IGetReviewSortTypeQueryHandler> _reviewSortHandler = new();
    private readonly Mock<ISetReviewSortTypeCmdHandler> _setReviewSortHandler = new();
    private readonly Mock<NavigationManager> _navigationManager = new();

    public SettingsTests()
    {
        Services.AddSingleton(_localizer.Object);
        Services.AddSingleton(_learnResetHandler.Object);
        Services.AddSingleton(_reviewResetHandler.Object);
        Services.AddSingleton(_learnSortHandler.Object);
        Services.AddSingleton(_setLearnSortHandler.Object);
        Services.AddSingleton(_reviewSortHandler.Object);
        Services.AddSingleton(_setReviewSortHandler.Object);
        
        // Mock JS Runtime for confirm
        JSInterop.Mode = JSRuntimeMode.Loose;
        
        _localizer.Setup(x => x[It.IsAny<string>()])
            .Returns((string key) => new LocalizedString(key, key));
    }

    [Fact]
    public void Should_Update_LearnSortType()
    {
        // Arrange
        _learnSortHandler.Setup(x => x.ExecuteAsync()).ReturnsAsync(WordSortType.Default);
        _reviewSortHandler.Setup(x => x.ExecuteAsync()).ReturnsAsync(WordSortType.Default);
        
        var cut = Render<Settings>();

        // Act
        var selects = cut.FindAll("select");
        selects[0].Change(nameof(WordSortType.Random));

        // Assert
        _setLearnSortHandler.Verify(x => x.ExecuteAsync(WordSortType.Random), Times.Once);
    }
    
    [Fact]
    public void Should_Update_ReviewSortType()
    {
        // Arrange
        _learnSortHandler.Setup(x => x.ExecuteAsync()).ReturnsAsync(WordSortType.Default);
        _reviewSortHandler.Setup(x => x.ExecuteAsync()).ReturnsAsync(WordSortType.Default);
        
        var cut = Render<Settings>();

        // Act
        var selects = cut.FindAll("select");
        selects[1].Change(nameof(WordSortType.AlphabeticalDesc));

        // Assert
        _setReviewSortHandler.Verify(x => x.ExecuteAsync(WordSortType.AlphabeticalDesc), Times.Once);
    }
}
