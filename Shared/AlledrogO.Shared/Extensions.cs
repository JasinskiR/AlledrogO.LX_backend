using AlledrogO.Shared.Authentication;
using AlledrogO.Shared.Commands;
using AlledrogO.Shared.Cors;
using AlledrogO.Shared.Database;
using AlledrogO.Shared.Exceptions;
using AlledrogO.Shared.Logging;
using AlledrogO.Shared.MassTransit;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace AlledrogO.Shared;

public static class Extensions
{
   public static IServiceCollection AddShared(this IServiceCollection services, IConfiguration configuration)
   {
       services.AddControllers();
       services.AddHttpContextAccessor();
       services.AddPostgres(configuration);
       services.AddCorsForAngular(configuration);
       services.AddCognitoAuthentication(configuration);
       services.AddSwaggerWithJwtAuth(configuration);
       services.AddSignalR();
       services.AddAuthorizationBuilder();
       services.AddMessageBroker();
       services.AddTransient<ExceptionMiddleware>();
       services.TryDecorate(typeof(ICommandHandler<>), typeof(LoggingCommandHandlerDecorator<>));
       
       return services;
   }
    
   public static IApplicationBuilder UseShared(this IApplicationBuilder app)
   {
       app.UseRouting();
       app.UseDefaultFiles();
       app.UseStaticFiles();
       app.UseCorsForAngular();
       app.UseAuthentication();
       app.UseAuthorization();
       app.UseSwagger(c =>
       {
           c.RouteTemplate = "api/swagger/{documentName}/swagger.json";
       });
       app.UseSwaggerUI( c => 
       {
           c.SwaggerEndpoint("/api/swagger/v1/swagger.json", "API V1");
           c.RoutePrefix = "api/swagger";
       });
       app.UseMiddleware<ExceptionMiddleware>();
       return app;
   }
   
   
}