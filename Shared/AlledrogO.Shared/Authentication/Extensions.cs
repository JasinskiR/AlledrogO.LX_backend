using System.Net.Http.Json;
using System.Reflection;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace AlledrogO.Shared.Authentication;

public static class Extensions
{
    private const string CognitoUserPoolIdEnvironmentVariable = "COGNITO_USER_POOL_ID";
    private const string CognitoAuthorityEnvironmentVariable = "COGNITO_AUTHORITY";
    public static IServiceCollection AddCognitoAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        // Configure authentication
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.SaveToken = true;
            
            // Your Cognito configuration
            // var cognitoConfig = configuration.GetSection("AWS:Cognito").Get<CognitoConfiguration>()
            //     ?? throw new NullReferenceException("Cognito configuration is missing in appsettings.json");
            var cognitoConfig = new CognitoConfiguration
            {
                UserPoolId = Environment.GetEnvironmentVariable(CognitoUserPoolIdEnvironmentVariable)
                    ?? throw new NullReferenceException("Cognito User Pool ID is missing in environment variables"),
                Authority = Environment.GetEnvironmentVariable(CognitoAuthorityEnvironmentVariable)
                    ?? throw new NullReferenceException("Cognito Authority is missing in environment variables")
            };
            
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidIssuer = cognitoConfig.Authority,
                IssuerSigningKeyResolver = (token, securityToken, kid, parameters) =>
                {
                    // Fetch the signing keys from your JSON endpoint
                    var httpClient = new HttpClient();
                    var keys = httpClient.GetFromJsonAsync<JsonWebKeySet>(cognitoConfig.JwksUri)
                        .GetAwaiter()
                        .GetResult();

                    // Find the signing key that matches the token's 'kid'
                    var signingKey = keys.Keys.FirstOrDefault(k => k.Kid == kid);
                    if (signingKey == null)
                        throw new SecurityTokenSignatureKeyNotFoundException($"Unable to find signing key: {kid}");

                    // Convert the RSA parameters
                    var rsa = new RSAParameters
                    {
                        Modulus = Base64UrlEncoder.DecodeBytes(signingKey.N),
                        Exponent = Base64UrlEncoder.DecodeBytes(signingKey.E)
                    };

                    return new[] { new RsaSecurityKey(rsa) };
                }
            };
        });

        return services;
    }
    
    public static void AddSwaggerWithJwtAuth(this IServiceCollection services, IConfiguration configuration)
    {
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
    }
}