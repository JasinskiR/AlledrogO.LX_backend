using AlledrogO.Message.Core.EF;
using AlledrogO.Message.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AlledrogO.Message.Core.Repositories;

public class ChatRepository : IChatRepository
{
    private readonly MessageDbContext _dbContext;

    public ChatRepository(MessageDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Chat> GetByIdAsync(Guid id)
    {
        return await _dbContext.Chats
            .FirstOrDefaultAsync(chat => chat.Id == id);
    }

    public async Task<Chat> GetByUsersPairAsync(Guid advertiserId, Guid buyerId)
    {
        return await _dbContext.Chats
            .Where(chat => chat.AdvertiserId == advertiserId && chat.BuyerId == buyerId ||
                chat.AdvertiserId == buyerId && chat.BuyerId == advertiserId)
            .FirstOrDefaultAsync();
    }

    public async Task CreateAsync(Chat chat)
    {
        await _dbContext.Chats.AddAsync(chat);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Chat chat)
    {
        _dbContext.Chats.Update(chat);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Chat chat)
    {
        _dbContext.Chats.Remove(chat);
        await _dbContext.SaveChangesAsync();
    }
}