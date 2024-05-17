using AlledrogO.User.Core.EF.Config;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AlledrogO.User.Core.EF.Contexts;

public class UserDbContext : IdentityDbContext<Entities.User>
{
    public UserDbContext(DbContextOptions<UserDbContext> options)
    : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("UsersSchema");
        
        var configuration = new UserConfiguration();
        modelBuilder.ApplyConfiguration<Entities.User>(configuration);
        
        base.OnModelCreating(modelBuilder);
    }
}