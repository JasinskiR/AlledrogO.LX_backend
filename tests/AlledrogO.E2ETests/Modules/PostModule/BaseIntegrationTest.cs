using System.Net.Http.Json;
using System.Text.Json;
using AlledrogO.Shared.Commands;
using AlledrogO.Shared.Queries;
using AlledrogO.User.Core.EF.Contexts;
using Microsoft.Extensions.DependencyInjection;

namespace AlledrogO.E2ETests.Modules.PostModule;

public abstract class BaseIntegrationTest
    : IClassFixture<WebAppFactory>,
        IDisposable
{
    private readonly IServiceScope _scope;
    protected readonly ICommandDispatcher CommandDispatcher;
    protected readonly IQueryDispatcher QueryDispatcher;
    protected readonly UserDbContext DbContext;
    protected HttpClient HttpClient { get; init; }

    protected BaseIntegrationTest(WebAppFactory factory)
    {
        HttpClient = factory.CreateClient();
        _scope = factory.Services.CreateScope();

        CommandDispatcher = _scope.ServiceProvider.GetRequiredService<ICommandDispatcher>();
        QueryDispatcher = _scope.ServiceProvider.GetRequiredService<IQueryDispatcher>();

        DbContext = _scope.ServiceProvider
            .GetRequiredService<UserDbContext>();
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
        var token = JsonSerializer.Deserialize<Dictionary<string, string>>(responseContent)!["accessToken"];
        return token;
    }

    public void Dispose()
    {
        _scope?.Dispose();
        DbContext?.Dispose();
    }
}