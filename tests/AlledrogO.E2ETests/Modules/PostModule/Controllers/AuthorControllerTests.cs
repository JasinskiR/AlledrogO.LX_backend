using System.Net;
using System.Net.Http.Json;
using AlledrogO.Post.Application.DTOs;
using AlledrogO.Post.Domain.Entities;
using AlledrogO.Post.Domain.Factories;
using AlledrogO.Post.Domain.ValueObjects;
using Shouldly;

namespace AlledrogO.E2ETests.Modules.PostModule.Controllers;

public class AuthorControllerTests : BaseIntegrationTest
{
    private Author SampleAuthor { get; set; }
    private AuthorFactory AuthorFactory { get; set; }
    public AuthorControllerTests(WebAppFactory factory) : base(factory)
    {
        AuthorFactory = new AuthorFactory();
        SampleAuthor = AuthorFactory.Create(
            Guid.Empty,
            new AuthorDetails("user@email.com", "123123123"),
            Enumerable.Empty<Post.Domain.Entities.Post>());
    }
    
    [Fact]
    public async Task GetAllAuthors_ShouldReturnAllAuthors()
    {
        // Arrange

        // Act
        var response = await HttpClient.GetAsync("api/Author");
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task GetInfo_ShouldReturnUnauthorized_WhenNoAccessToken()
    {
        // Arrange

        // Act
        var response = await HttpClient.GetAsync("api/Author/info");
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }
    
    [Fact]
    public async Task GetInfo_ShouldReturnAuthorInfo_WhenAccessTokenProvided()
    {
        // Arrange
        await GenerateSampleUserAsync();
        var token = await GetSampleAccessTokenAsync();
        HttpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        var authorDto = new AuthorDto()
        {
            Details = new AuthorDetailsDto()
            {
                Email = SampleAuthor.AuthorDetails.Email,
                PhoneNumber = SampleAuthor.AuthorDetails.PhoneNumber
            }
        };
        
        // Act
        var response = await HttpClient.GetAsync("api/Author/info");
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var responseAuthorDto = await response.Content.ReadFromJsonAsync<AuthorDto>();
        responseAuthorDto.Details.ShouldBeEquivalentTo(authorDto.Details);
        responseAuthorDto.Posts.ShouldBeEmpty();
    }
}