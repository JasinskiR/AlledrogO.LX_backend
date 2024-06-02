using AlledrogO.Shared.Commands;
using AlledrogO.Shared.Cors;
using AlledrogO.Shared.Database;
using AlledrogO.Shared.Exceptions;
using AlledrogO.Shared.Logging;
using AlledrogO.Shared.MassTransit;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
       services.AddSwaggerGen(swagger =>
       {
           swagger.EnableAnnotations();
           swagger.CustomSchemaIds(x => x.FullName);
           swagger.SwaggerDoc("v1", new OpenApiInfo
           {
               Title = "Alledrogo API",
               Version = "v1"
           });
       });
       services.ConfigureApplicationCookie(options =>
       {
           options.ExpireTimeSpan = TimeSpan.FromHours(8);
           options.SlidingExpiration = true;
           options.Cookie.SameSite = SameSiteMode.None;
           options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
           options.Cookie.HttpOnly = false;
       });
       services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme);
           // .AddCookie(options =>
           // {
           //     options.Cookie.SameSite = SameSiteMode.None;
           //      options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
           // });
       
       services.AddAuthorizationBuilder();
       services.AddMessageBroker();
       services.AddTransient<ExceptionMiddleware>();
       services.TryDecorate(typeof(ICommandHandler<>), typeof(LoggingCommandHandlerDecorator<>));
       return services;
   }
    
   public static IApplicationBuilder UseShared(this IApplicationBuilder app)
   {
       app.UseDefaultFiles();
       app.UseStaticFiles();
       app.UseCorsForAngular();
       app.UseAuthentication();
       app.UseAuthorization();
       app.UseSwagger();
       app.UseSwaggerUI();
       app.UseMiddleware<ExceptionMiddleware>();
       return app;
   }
   
   
}