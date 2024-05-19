using AlledrogO.Shared.Commands;
using AlledrogO.Shared.Exceptions;
using AlledrogO.Shared.Logging;
using AlledrogO.Shared.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace AlledrogO.Shared;

public static class Extensions
{
   public static IServiceCollection AddShared(this IServiceCollection services)
   {
       
       services.AddTransient<ExceptionMiddleware>();
       services.TryDecorate(typeof(ICommandHandler<>), typeof(LoggingCommandHandlerDecorator<>));
       return services;
   }
    
   public static IApplicationBuilder UseShared(this IApplicationBuilder app)
   {
       app.UseMiddleware<ExceptionMiddleware>();
       return app;
   }
   
   
}