using System.Net;
using System.Net.Http.Json;
using api.Models.Entities;


namespace tests;

public class PageTests : IClassFixture<TestDatabaseFactory>
{
    private readonly TestDatabaseFactory _factory;

    public PageTests(TestDatabaseFactory factory)
    {
        _factory = factory;
    }
    
    [Fact]
    public async Task Get_Pages_Endpoint_Should_Return_OK()
    {
        //Arrange
        var client = _factory.CreateClient();
        
        // Act
        var response = await client.GetAsync("/api/search?q=javascript");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    
    [Fact]
    public async Task Ok()
    {
        //Arrange
        var client = _factory.CreateClient();
        
        // Act
        var response = await client.GetAsync("/api/search?q=Script");
        var pages = await response.Content.ReadFromJsonAsync<List<Page>>();
        
        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(pages);
        Assert.Equal(2, pages.Count);
    }
}