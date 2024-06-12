using System.Net;
using AlledrogO.Post.Application.DTOs;
using Microsoft.AspNetCore.Http;
using Shouldly;

namespace AlledrogO.E2ETests.Modules.UserModule;

public class SampleTests : BaseIntegrationTest
{
    public SampleTests(WebAppFactory factory) : base(factory)
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
}