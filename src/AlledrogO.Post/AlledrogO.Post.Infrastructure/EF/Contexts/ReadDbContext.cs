using AlledrogO.Post.Infrastructure.EF.Config;
using AlledrogO.Post.Infrastructure.EF.Models;
using AlledrogO.Post.Infrastructure.EF.Models.ReadModels;
using Microsoft.EntityFrameworkCore;

namespace AlledrogO.Post.Infrastructure.EF.Contexts;

public class ReadDbContext : DbContext
{
    public DbSet<PostReadDbModel> Posts { get; set; }
    public DbSet<AuthorReadDbModel> Authors { get; set; }
    public DbSet<TagReadDbModel> Tags { get; set; }
    public ReadDbContext(DbContextOptions<ReadDbContext> options) 
        : base(options)
    {
    }
    
protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("PostSchema");
        
        var configuration = new ReadConfiguration();
        modelBuilder.ApplyConfiguration<PostReadDbModel>(configuration);
        modelBuilder.ApplyConfiguration<AuthorReadDbModel>(configuration);
        modelBuilder.ApplyConfiguration<TagReadDbModel>(configuration);
        modelBuilder.ApplyConfiguration<PostImageReadDbModel>(configuration);
        
        base.OnModelCreating(modelBuilder);
    }
    
    
}