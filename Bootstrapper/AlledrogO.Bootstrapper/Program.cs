using AlledrogO.Message.Api;
using AlledrogO.Post.Api;
using AlledrogO.Shared;
using AlledrogO.User.Api;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://*:8080");
builder.Services.AddHealthChecks();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddShared(builder.Configuration);
builder.Services.AddUserModule();
builder.Services.AddPostModule(builder.Configuration);
builder.Services.AddMessageModule(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseShared();
app.UseHttpsRedirection();
app.MapHealthChecks("/api/health");
app.MapControllers();
app.UseUserModule();
app.UseMessageModule();
app.MapGet("/", () => Results.Redirect("/api/swagger/index.html"))
    .Produces(200)
    .ExcludeFromDescription();

app.Run();
public partial class Program
{
}