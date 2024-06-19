using AlledrogO.Message.Core.DTOs;
using AlledrogO.Message.Core.EF;
using AlledrogO.Message.Core.Entities;
using AlledrogO.Shared.Queries;
using Microsoft.EntityFrameworkCore;

namespace AlledrogO.Message.Core.Queries.Handlers;

public class GetChatsForUserHandler : IQueryHandler<GetChatsForUser, IEnumerable<ChatDetailsDto>>
{
    private readonly DbSet<Chat> _chats;
    
    public GetChatsForUserHandler(MessageDbContext dbContext)
    {
        _chats = dbContext.Set<Chat>();
    }

    public async Task<IEnumerable<ChatDetailsDto>> HandleAsync(GetChatsForUser query)
    {
        return await _chats
            .Where(chat => chat.AdvertiserId == query.UserId || chat.BuyerId == query.UserId)
            .Select(c => c.AsDto())
            .AsNoTracking()
            .ToListAsync();
    }
}