using AlledrogO.Message.Core.DTOs;
using AlledrogO.Message.Core.EF;
using AlledrogO.Message.Core.Entities;
using AlledrogO.Shared.Queries;
using Microsoft.EntityFrameworkCore;

namespace AlledrogO.Message.Core.Queries.Handlers;

public class GetChatByIdHandler : IQueryHandler<GetChatById, ChatDto>
{
    private readonly DbSet<Chat> _chats;
    
    public GetChatByIdHandler(MessageDbContext dbContext)
    {
        _chats = dbContext.Set<Chat>();
    }
    public async Task<ChatDto> HandleAsync(GetChatById query)
    {
        return await _chats
            .Where(chat => chat.Id == query.ChatId)
            .Select(c => c.AsDto())
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }
}