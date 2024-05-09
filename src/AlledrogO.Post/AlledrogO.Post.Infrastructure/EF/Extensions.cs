using AlledrogO.Post.Application.Contracts;
using AlledrogO.Post.Infrastructure.EF.Options;
using AlledrogO.Post.Infrastructure.EF.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AlledrogO.Post.Infrastructure.EF;

public static class Extensions
{
    public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPostRepository, PostgresPostRepository>();

        // var options = configuration.GetOptions<PostgresOptions>("Postgres")
        //     ?? throw new Exception("Postgres options not found");
        // services.AddDbContext<PostDbContext>(ctx =>
        //     ctx.UseNpgsql(options.ConnectionString));
        return services;
    }

}