using AlledrogO.Post.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace AlledrogO.Post.Infrastructure.EF.Contexts;

public class ReadDbContext : DbContext
{
    public DbSet<PostReadDbModel> Posts { get; set; }
    
    public ReadDbContext(DbContextOptions<ReadDbContext> options) 
        : base(options)
    {
    }
    
    
}