using AlledrogO.Post.Api;
using AlledrogO.Shared;
using AlledrogO.Shared.Database;
using AlledrogO.User.Api;
using AlledrogO.User.Core.Entities;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddShared(builder.Configuration);
builder.Services.AddUserModule();
builder.Services.AddPostModule(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseShared();
app.UseHttpsRedirection();
app.MapControllers();
app.UseUserModule();
app.MapGet("/", () => Results.Redirect("/swagger/index.html"))
    .Produces(200)
    .ExcludeFromDescription();

app.Run();
