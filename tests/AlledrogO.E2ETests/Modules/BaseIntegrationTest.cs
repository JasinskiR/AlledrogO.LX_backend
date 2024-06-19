using System.Net.Http.Json;
using System.Text.Json;
using AlledrogO.Shared.Commands;
using AlledrogO.Shared.Queries;
using AlledrogO.User.Core.EF.Contexts;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.Extensions.DependencyInjection;

namespace AlledrogO.E2ETests.Modules;

public abstract class BaseIntegrationTest
    : IClassFixture<WebAppFactory>,
        IDisposable
{
    private readonly IServiceScope _scope;
    protected HttpClient HttpClient { get; init; }

    protected BaseIntegrationTest(WebAppFactory factory)
    {
        HttpClient = factory.CreateClient();
        _scope = factory.Services.CreateScope();
        
    }
    
    protected async Task GenerateSampleUserAsync()
    {
        var response = await HttpClient.PostAsJsonAsync("api/User/register", new
        {
            email = "user@email.com",
            password = "Qwerty123@#",
            phoneNumber = "123123123"
        });
        response.EnsureSuccessStatusCode();
        
    }

    internal class TokenResponse
    {
        public string tokenType { get; set; }
        public string accessToken { get; set; }
        public int expiresIn { get; set; }
        public string refreshToken { get; set; }
    }
    
    protected async Task<string> GetSampleAccessTokenAsync()
    {
        var response = await HttpClient.PostAsJsonAsync("api/User/login", new
        {
            email = "user@email.com",
            password = "Qwerty123@#",
            rememberMe = true
        });

        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();
        var token = JsonSerializer.Deserialize<TokenResponse>(responseContent)!.accessToken;
        return token;
    }

    public void Dispose()
    {
        _scope?.Dispose();
    }
}