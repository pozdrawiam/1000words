using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Otw.Core.Domain;

namespace Otw.WebApp.Tests.Pages;

public class ListTests : TestContext
{
    private readonly Mock<IWordsRepository> _repoMock;
    
    public ListTests()
    {
        _repoMock = new();
        Services.AddSingleton(_repoMock.Object);
    }
    
    [Fact]
    public void Should_ShowLoadingMessage_BeforeDataIsLoaded()
    {
        _repoMock.Setup(r => r.GetAllAsync())
            .Returns(async () =>
            {
                await Task.Delay(1_000);
                return [];
            });

        // Act
        var cut = RenderComponent<Otw.WebApp.Pages.List>();
        
        cut.MarkupMatches("<h3>List</h3><p><em>Loading...</em></p>");
    }

    [Fact]
    public void Should_RenderTableWithWords_WhenRepositoryReturnsData()
    {
        var words = new WordEntity[]
        {
            new() { Id = 1, Value = "cat", Translation = "kot" },
            new() { Id = 2, Value = "dog", Translation = "pies" }
        };

        _repoMock.Setup(r => r.GetAllAsync())
            .ReturnsAsync(words);

        // Act
        var cut = RenderComponent<Otw.WebApp.Pages.List>();
        
        cut.MarkupMatches(@"
<h3>List</h3>
<table class=""table"">
  <tbody>
    <tr><td>cat</td><td>kot</td></tr>
    <tr><td>dog</td><td>pies</td></tr>
  </tbody>
</table>");
    }
}
