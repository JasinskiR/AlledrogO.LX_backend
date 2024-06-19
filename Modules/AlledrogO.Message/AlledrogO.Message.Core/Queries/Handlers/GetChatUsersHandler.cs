using AlledrogO.Message.Core.DTOs;
using AlledrogO.Message.Core.EF;
using AlledrogO.Message.Core.Entities;
using AlledrogO.Shared.Queries;
using Microsoft.EntityFrameworkCore;

namespace AlledrogO.Message.Core.Queries.Handlers;

public class GetChatUsersHandler : IQueryHandler<GetChatUsers, IEnumerable<ChatUserDto>>
{
    private readonly DbSet<ChatUser> _chatUsers;
    
    public GetChatUsersHandler(MessageDbContext context)
    {
        _chatUsers = context.Set<ChatUser>();
    }
    public async Task<IEnumerable<ChatUserDto>> HandleAsync(GetChatUsers query)
    {
        return await _chatUsers
            .Include(cu => cu.ChatsAsAdvertiser)
            .ThenInclude(c => c.Buyer)
            .Include(cu => cu.ChatsAsBuyer)
            .ThenInclude(c => c.Advertiser)
            .Select(chatUser => chatUser.AsDto())
            .AsNoTracking()
            .ToListAsync();
    }
}