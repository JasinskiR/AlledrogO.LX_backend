using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using AlledrogO.User.Api.DTOs;
using Shouldly;

namespace AlledrogO.E2ETests.Modules.UsersModule.Controllers;

public class UserControllerTests : BaseIntegrationTest
{
    public UserControllerTests(WebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Register_ShouldReturnOk_WhenValidData()
    {
        // Arrange
        var registerDto = new RegisterDto()
        {
            Email = "test1@email.com",
            Password = "Qwerty123@#",
            PhoneNumber = "123456789"
        };

        // Act
        var response = await HttpClient.PostAsJsonAsync("api/User/register", registerDto);
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task Login_ShouldReturnValidToken_WhenValidData()
    {
        // Arrange
        var registerDto = new RegisterDto()
        {
            Email = "test2@email.com",
            Password = "Qwerty123@#",
            PhoneNumber = "123456789"
        };
        await HttpClient.PostAsJsonAsync("api/User/register", registerDto);
        
        var loginDto = new LoginDto()
        {
            Email = "test2@email.com",
            Password = "Qwerty123@#",
            RememberMe = true
        };
        
        // Act
        var response = await HttpClient.PostAsJsonAsync("api/User/login", loginDto);
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        
        // Arrange 2
        var responseContent = await response.Content.ReadAsStringAsync();
        var token = JsonSerializer.Deserialize<TokenResponse>(responseContent)!.accessToken;
        token.ShouldNotBeEmpty();
        
        HttpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

        // Act 2
        var response2 = await HttpClient.GetAsync("api/User/test");
        
        // Assert 2
        response2.StatusCode.ShouldBe(HttpStatusCode.OK);
        
    }
}