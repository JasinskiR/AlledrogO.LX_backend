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
           swagger.AddSecurityDefinition("Bearer",
               new OpenApiSecurityScheme
               {
                   Description =
                       "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                   Name = "Authorization",
                   In = ParameterLocation.Header,
                   Type = SecuritySchemeType.ApiKey,
                   Scheme = "Bearer"
               });

           swagger.AddSecurityRequirement(new OpenApiSecurityRequirement()
           {
               {
                   new OpenApiSecurityScheme
                   {
                       Reference = new OpenApiReference
                       {
                           Type = ReferenceType.SecurityScheme,
                           Id = "Bearer"
                       },
                       Scheme = "oauth2",
                       Name = "Bearer",
                       In = ParameterLocation.Header,

                   },
                   new List<string>()
               }
           });
       });
       services.AddAuthorizationBuilder();
       services.AddAuthentication();
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