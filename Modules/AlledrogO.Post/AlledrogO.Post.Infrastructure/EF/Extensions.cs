using AlledrogO.Post.Application.Contracts;
using AlledrogO.Post.Infrastructure.EF.Contexts;
using AlledrogO.Post.Infrastructure.EF.Repositories;
using AlledrogO.Shared.Database;
using AlledrogO.Shared.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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