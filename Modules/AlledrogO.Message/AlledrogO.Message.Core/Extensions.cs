using AlledrogO.Message.Core.EF;
using AlledrogO.Message.Core.Entities;
using AlledrogO.Message.Core.Repositories;
using AlledrogO.Shared.Commands;
using AlledrogO.Shared.Database;
using AlledrogO.Shared.Queries;
using Amazon.Extensions.NETCore.Setup;
using Amazon.SQS;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AlledrogO.Message.Core;

public static class Extensions
{
    public static IServiceCollection AddMessageCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCommands();
        services.AddQueries();
        services.AddPostgres<MessageDbContext>();
        services.AddScoped<IChatUserRepository, ChatUserRepository>();
        services.AddScoped<IChatRepository, ChatRepository>();
        services.AddHostedService<PlatformUserInitializer>();
        AWSOptions awsOptions = configuration.GetAWSOptions();
        services.AddDefaultAWSOptions(awsOptions);
        services.AddAWSService<IAmazonSQS>();
        
        return services;
    }
}