using System.Net;
using Shouldly;

namespace AlledrogO.E2ETests.Modules.PostModule.Controllers;

public class AuthorControllerTests : BaseIntegrationTest
{
    public AuthorControllerTests(WebAppFactory factory) : base(factory)
    {
    }
    
    [Fact]
    public async Task GetAllAuthors_ShouldReturnAllAuthors()
    {
        // Arrange

        // Act
        var response = await HttpClient.GetAsync("api/Author");
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        // response.Content.ShouldBeEquivalentTo(Enumerable.Empty<AuthorDto>());
    }
    
    [Fact]
    public async Task GetInfo_IfNoAccessToken_ShouldReturnUnauthorized()
    {
        // Arrange

        // Act
        var response = await HttpClient.GetAsync("api/Author/info");
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }
    
    [Fact]
    public async Task GetInfo_IfAccessToken_ShouldReturnAuthorInfo()
    {
        // Arrange
        await GenerateSampleUserAsync();
        var token = await GetSampleAccessTokenAsync();
        HttpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        
        // Act
        var response = await HttpClient.GetAsync("api/Author/info");
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
}