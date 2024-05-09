using System.Reflection;
using AlledrogO.Shared.Commands;
using AlledrogO.Shared.Queries;
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
}