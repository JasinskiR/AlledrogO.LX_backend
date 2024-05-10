using AlledrogO.Post.Application.Contracts;
using AlledrogO.Post.Infrastructure.EF.Contexts;
using AlledrogO.Post.Infrastructure.EF.Options;
using AlledrogO.Post.Infrastructure.EF.Repositories;
using AlledrogO.Shared.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AlledrogO.Post.Infrastructure.EF;

public static class Extensions
{
    public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPostRepository, PostgresPostRepository>();
        services.AddScoped<IAuthorRepository, PostgresAuthorRepository>();
        services.AddScoped<ITagRepository, PostgresTagRepository>();

        var options = configuration.GetOptions<PostgresOptions>("Postgres")
            ?? throw new Exception("Postgres options not found");
        
        services.AddDbContext<WriteDbContext>(ctx =>
            ctx.UseNpgsql(options.ConnectionString));
        services.AddDbContext<ReadDbContext>(ctx =>
            ctx.UseNpgsql(options.ConnectionString));
        return services;
    }

}