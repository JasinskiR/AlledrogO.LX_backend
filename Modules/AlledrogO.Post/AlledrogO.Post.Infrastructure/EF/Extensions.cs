using AlledrogO.Post.Application.Contracts;
using AlledrogO.Post.Infrastructure.EF.Contexts;
using AlledrogO.Post.Infrastructure.EF.Repositories;
using AlledrogO.Shared.Database;
using Microsoft.Extensions.DependencyInjection;

namespace AlledrogO.Post.Infrastructure.EF;

internal static class Extensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddScoped<IPostRepository, PostgresPostRepository>();
        services.AddScoped<IAuthorRepository, PostgresAuthorRepository>();
        services.AddScoped<ITagRepository, PostgresTagRepository>();
        
        services.AddPostgres<WriteDbContext>();
        services.AddPostgres<ReadDbContext>();
        return services;
    }
    
    

}