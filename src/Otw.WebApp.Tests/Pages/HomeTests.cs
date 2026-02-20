using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Otw.Core.Domain;
using Otw.WebApp.Pages;

namespace Otw.WebApp.Tests.Pages;

public class HomeTests : BunitContext
{
    private readonly Mock<IParametersRepository> _parametersRepository = new();

    public HomeTests()
    {
        Services.AddSingleton(_parametersRepository.Object);
    }

    [Fact]
    public void Should_RedirectToLearn_When_LearnStarted()
    {
        // Arrange
        _parametersRepository.Setup(x => x.IsLearnStartedAsync()).ReturnsAsync(true);
        var nav = Services.GetRequiredService<NavigationManager>();

        // Act
        Render<Home>();

        // Assert
        Assert.Equal("http://localhost/learn", nav.Uri);
    }

    [Fact]
    public void Should_RedirectToAbout_When_LearnNotStarted()
    {
        // Arrange
        _parametersRepository.Setup(x => x.IsLearnStartedAsync()).ReturnsAsync(false);
        var nav = Services.GetRequiredService<NavigationManager>();

        // Act
        Render<Home>();

        // Assert
        Assert.Equal("http://localhost/about", nav.Uri);
    }
}
