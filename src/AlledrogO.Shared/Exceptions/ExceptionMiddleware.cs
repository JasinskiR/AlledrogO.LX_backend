using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;

namespace AlledrogO.Shared.Exceptions;

public class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (AlledrogoException ex)
        {
            context.Response.StatusCode = 400;
            context.Response.ContentType = "application/json";
            
            var errorCode = ToSnakeCase(ex.GetType().Name.Replace("Exception", ""));
            var json = JsonSerializer.Serialize(new { error = errorCode, message = ex.Message });
            await context.Response.WriteAsync(json);
        }

    }

    private static string ToSnakeCase(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        var startUnderscores = Regex.Match(input, @"^_+");
        return startUnderscores + Regex.Replace(input, @"([a-z0-9])([A-Z])", "$1_$2").ToLower();
    }
}