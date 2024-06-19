using AlledrogO.Message.Core.DTOs;
using AlledrogO.Message.Core.EF;
using AlledrogO.Message.Core.Entities;
using AlledrogO.Shared.Queries;
using Microsoft.EntityFrameworkCore;

namespace AlledrogO.Message.Core.Queries.Handlers;

public class GetChatByIdHandler : IQueryHandler<GetChatById, ChatDetailsDto>
{
    private readonly DbSet<Chat> _chats;
    private readonly DbSet<ChatUser> _chatUsers;
    
    public GetChatByIdHandler(MessageDbContext dbContext)
    {
        _chats = dbContext.Set<Chat>();
        _chatUsers = dbContext.Set<ChatUser>();
    }
    public async Task<ChatDetailsDto> HandleAsync(GetChatById query)
    {
        return await _chats
            .Where(chat => chat.Id == query.ChatId)
            .Include(chat => chat.Buyer)
            .Include(chat => chat.Advertiser)
            .Select(c => c.AsDto())
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }
}