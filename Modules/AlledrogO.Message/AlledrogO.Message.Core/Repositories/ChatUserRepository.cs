using AlledrogO.Message.Core.EF;
using AlledrogO.Message.Core.Entities;

namespace AlledrogO.Message.Core.Repositories;

public class ChatUserRepository : IChatUserRepository
{
    private readonly MessageDbContext _context;

    public ChatUserRepository(MessageDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task<ChatUser> GetByIdAsync(Guid id)
    {
        return await _context.ChatUsers.FindAsync(id);
    }

    public async Task CreateAsync(ChatUser chatUser)
    {
        await _context.ChatUsers.AddAsync(chatUser);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ChatUser chatUser)
    {
        _context.ChatUsers.Update(chatUser);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(ChatUser chatUser)
    {
        _context.ChatUsers.Remove(chatUser);
        await _context.SaveChangesAsync();
    }
}