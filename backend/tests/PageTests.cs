using System.Net;
using System.Net.Http.Json;
using api.Models.Entities;
using DotNetEnv;


namespace tests;

public class PageTests : IClassFixture<TestDatabaseFactory>
{
    private readonly TestDatabaseFactory _factory;

    public PageTests(TestDatabaseFactory factory)
    {
        _factory = factory;
        
        // Define the path to the .env file in the "api" project
        var apiProjectPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName,
            "..", "api", ".env");

        if (File.Exists(apiProjectPath))
        {
            Env.Load(apiProjectPath); // Load .env file explicitly
        }
        else
        {
            throw new FileNotFoundException(".env file not found at " + apiProjectPath);
        }
    }
    
    [Fact]
    public async Task Search_Endpoint_Should_Return_OK()
    {
        //Arrange
        var client = _factory.CreateClient();
        
        // Act
        var response = await client.GetAsync("/api/search?q=JavaScript");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    
    [Fact]
    public async Task Search_Endpoint_Should_Return_Two_Results_On_Script_Search()
    {
        //Arrange
        var client = _factory.CreateClient();
        
        // Act
        var response = await client.GetAsync("/api/search?q=script");
        var pages = await response.Content.ReadFromJsonAsync<List<Page>>();
        
        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(pages);
        Assert.Equal(2, pages.Count);
        foreach (var page in pages)
        {
            Assert.Contains("script", page.Content);
        }
    }
    
    [Fact]
    public async Task Search_Endpoint_Should_Return_OK_On_No_Search_Match()
    {
        //Arrange
        var client = _factory.CreateClient();
        
        // Act
        var response = await client.GetAsync("/api/search?q=leverpostej");
        var pages = await response.Content.ReadFromJsonAsync<List<Page>>();
        
        // Assert
        Assert.Equal([], pages);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}