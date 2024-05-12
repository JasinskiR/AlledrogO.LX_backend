using AlledrogO.Post.Application;
using AlledrogO.Post.Domain.Entities;
using AlledrogO.Post.Domain.Factories;
using AlledrogO.Post.Domain.ValueObjects;
using AlledrogO.Post.Infrastructure;
using AlledrogO.Post.Infrastructure.EF.Contexts;
using AlledrogO.Post.Infrastructure.EF.Options;
using AlledrogO.Shared;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddShared();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     
// }

app.UseSwagger();
app.UseSwaggerUI();
app.MapGet("/", () => Results.Redirect("/swagger/index.html"))
    .Produces(200)
    .ExcludeFromDescription();
app.UseShared();
app.UseHttpsRedirection();
app.MapControllers();

// testDb(builder, app);


app.Run();

void testDb(WebApplicationBuilder webApplicationBuilder, WebApplication webApplication)
{
    var options = new DbContextOptionsBuilder<WriteDbContext>()
        .UseNpgsql(webApplicationBuilder.Configuration.GetConnectionString("Postgres"))
        .Options;

    var authorFactory = new AuthorFactory();
    var author1 = authorFactory.Create(
        new Guid(), 
        new AuthorDetails("author1@mail.com", "123456789"),
        Enumerable.Empty<Post>());
    
    var tagFactory = new TagFactory();
    var tag1 = tagFactory.Create(new Guid(), "tag1", Enumerable.Empty<Post>());
    var tag2 = tagFactory.Create(new Guid(), "tag2", Enumerable.Empty<Post>());

    var postFactory = new PostFactory();
    var post1 = postFactory.Create(
        new Guid(), 
        "title1", 
        "description1", 
        author1);
    post1.AddTag(tag1);
    post1.AddTag(tag2);

    using (var scope = webApplication.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<WriteDbContext>();
        // context.Database.EnsureDeleted();
        // context.Database.EnsureCreated();
    
        context.Authors.Add(author1);
        context.SaveChanges();
        
        context.Tags.Add(tag1);
        context.Tags.Add(tag2);
        context.SaveChanges();
        
        context.Posts.Add(post1);
        context.SaveChanges();
    }

    // using (var scope = webApplication.Services.CreateScope())
    // {
    //     var context = scope.ServiceProvider.GetRequiredService<WriteDbContext>();
    //
    //     var result = context.Authors.FirstOrDefault(a => a.Id == author1.Id);
    //     if (result == null)
    //     {
    //         Console.WriteLine("Author not found");
    //     }
    //     Console.WriteLine(result?.AuthorDetails.Email);
    // }
    //
    // using (var scope = webApplication.Services.CreateScope())
    // {
    //     var context = scope.ServiceProvider.GetRequiredService<ReadDbContext>();
    //
    //     var result = context.Authors.FirstOrDefault(a => a.Id == author1.Id);
    //     if (result == null)
    //     {
    //         Console.WriteLine("Author not found");
    //     }
    //     Console.WriteLine($"Found in read context: {result?.AuthorDetails}");
    // }
}
