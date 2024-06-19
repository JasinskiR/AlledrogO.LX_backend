using AlledrogO.Message.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AlledrogO.Message.Core.EF;

public class MessageDbContext : DbContext
{
    public DbSet<Chat> Chats { get; set; }
    public DbSet<ChatUser> ChatUsers { get; set; }
    
    public MessageDbContext(DbContextOptions<MessageDbContext> options) 
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("MessageSchema");
        
        var configuration = new MessageConfiguration();
        modelBuilder.ApplyConfiguration<Chat>(configuration);
        modelBuilder.ApplyConfiguration<ChatUser>(configuration);
        
        base.OnModelCreating(modelBuilder);
    }

}