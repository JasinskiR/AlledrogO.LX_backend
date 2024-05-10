using System.Reflection;
using AlledrogO.Shared.Commands;
using AlledrogO.Shared.Exceptions;
using AlledrogO.Shared.Queries;
using AlledrogO.Shared.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace AlledrogO.Shared;

public static class Extensions
{
   public static IServiceCollection AddCommands(this IServiceCollection services)
   {
       var assembly = Assembly.GetCallingAssembly();
       
       services.AddSingleton<ICommandDispatcher, InMemoryCommandDispatcher>();
       services.Scan(s => s.FromAssemblies(assembly)
          .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
          .AsImplementedInterfaces()
          .WithScopedLifetime());
       return services;
   } 
   
   public static IServiceCollection AddQueries(this IServiceCollection services)
   {
       var assembly = Assembly.GetCallingAssembly();
       
       services.AddSingleton<IQueryDispatcher, InMemoryQueryDispatcher>();
       services.Scan(s => s.FromAssemblies(assembly)
          .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
          .AsImplementedInterfaces()
          .WithScopedLifetime());
       return services;
   }
   
   public static IServiceCollection AddShared(this IServiceCollection services)
   {
       services.AddTransient<ExceptionMiddleware>();
       services.AddHostedService<AppInitializer>();
       return services;
   }
    
   public static IApplicationBuilder UseShared(this IApplicationBuilder app)
   {
       app.UseMiddleware<ExceptionMiddleware>();
       return app;
   }
}