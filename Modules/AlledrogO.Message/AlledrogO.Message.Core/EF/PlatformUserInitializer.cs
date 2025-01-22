using AlledrogO.Message.Core.Entities;
using AlledrogO.Message.Core.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AlledrogO.Message.Core.EF;

public class PlatformUserInitializer : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IConfiguration _configuration;
    private readonly ILogger<PlatformUserInitializer> _logger;

    public PlatformUserInitializer(IServiceProvider serviceProvider, IConfiguration configuration, 
        ILogger<PlatformUserInitializer> logger)
    {
        _serviceProvider = serviceProvider;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        try
        {
            var platformEmail = _configuration.GetSection("PlatformEmail").Value
                                ?? throw new ArgumentNullException("PlatformEmail is not set in appsettings.json");
            var chatUserRepository = scope.ServiceProvider.GetRequiredService<IChatUserRepository>();
            var platformUser = await chatUserRepository.GetByEmailAsync(platformEmail);
            if (platformUser is null)
            {
                var chatUser = new ChatUser()
                {
                    Id = Guid.NewGuid(),
                    Email = platformEmail,
                    ChatsAsBuyer = new List<Chat>(),
                    ChatsAsAdvertiser = new List<Chat>()
                };
                await chatUserRepository.CreateAsync(chatUser);
            }
        }
        catch (Exception e)
        {
            _logger.LogWarning(e, "An error occurred while initializing platform user");
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}