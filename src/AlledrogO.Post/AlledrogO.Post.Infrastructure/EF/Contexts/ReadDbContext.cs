using AlledrogO.Post.Infrastructure.EF.Config;
using AlledrogO.Post.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace AlledrogO.Post.Infrastructure.EF.Contexts;

public class ReadDbContext : DbContext
{
    public DbSet<PostDbModel> Posts { get; set; }
    public DbSet<AuthorDbModel> Authors { get; set; }
    public DbSet<TagDbModel> Tags { get; set; }
    public ReadDbContext(DbContextOptions<ReadDbContext> options) 
        : base(options)
    {
    }
    
protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("PostSchema");
        
        var configuration = new ReadConfiguration();
        modelBuilder.ApplyConfiguration<PostDbModel>(configuration);
        modelBuilder.ApplyConfiguration<AuthorDbModel>(configuration);
        modelBuilder.ApplyConfiguration<TagDbModel>(configuration);
        modelBuilder.ApplyConfiguration<PostImageDbModel>(configuration);
        
        base.OnModelCreating(modelBuilder);
    }
    
    
}