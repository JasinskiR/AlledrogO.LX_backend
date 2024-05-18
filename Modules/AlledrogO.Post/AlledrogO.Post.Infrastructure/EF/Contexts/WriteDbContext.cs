using AlledrogO.Post.Domain.Entities;
using AlledrogO.Post.Infrastructure.EF.Config;
using Microsoft.EntityFrameworkCore;

namespace AlledrogO.Post.Infrastructure.EF.Contexts;

public class WriteDbContext : DbContext
{
    public DbSet<Domain.Entities.Post> Posts { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<PostImage> PostImages { get; set; }
    
    public WriteDbContext(DbContextOptions<WriteDbContext> options) 
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("PostSchema");
        
        var configuration = new WriteConfiguration();
        modelBuilder.ApplyConfiguration<Domain.Entities.Post>(configuration);
        modelBuilder.ApplyConfiguration<Author>(configuration);
        modelBuilder.ApplyConfiguration<Tag>(configuration);
        modelBuilder.ApplyConfiguration<PostImage>(configuration);
        
        base.OnModelCreating(modelBuilder);
    }
}