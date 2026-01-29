using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Moq;
using Otw.Core.Domain;
using Otw.WebApp.Pages;

namespace Otw.WebApp.Tests.Pages;

public class AboutTests : TestContext
{
    private readonly Mock<IStringLocalizer<Resources.Pages.About>> _localizer = new();
    private readonly Mock<IParametersRepository> _parametersRepository = new();

    public AboutTests()
    {
        Services.AddSingleton(_localizer.Object);
        Services.AddSingleton(_parametersRepository.Object);
        
        _localizer.Setup(x => x[It.IsAny<string>()])
            .Returns((string key) => new LocalizedString(key, key));
    }

    [Fact]
    public void Should_RenderContent()
    {
        // Act
        var cut = RenderComponent<About>();

        // Assert
        Assert.Contains("Title", cut.Markup);
        Assert.Contains("GitHubLink", cut.Markup);
    }

    [Fact]
    public void Should_StartLearning_OnClick()
    {
        // Arrange
        var nav = Services.GetRequiredService<NavigationManager>();

        var cut = RenderComponent<About>();
        
        // Act
        cut.Find("button.btn-primary").Click();

        // Assert
        _parametersRepository.Verify(x => x.SetLearnStartedAsync(), Times.Once);
        Assert.Equal("http://localhost/learn", nav.Uri);
    }
}
