using AlledrogO.Message.Core.DTOs;
using AlledrogO.Message.Core.EF;
using AlledrogO.Message.Core.Entities;
using AlledrogO.Shared.Queries;
using Microsoft.EntityFrameworkCore;

namespace AlledrogO.Message.Core.Queries.Handlers;

public class GetChatUserByIdHandler : IQueryHandler<GetChatUserById, ChatUserDto>
{
    private readonly DbSet<ChatUser> _chatUsers;

    public GetChatUserByIdHandler(MessageDbContext context)
    {
        _chatUsers = context.Set<ChatUser>();
    }

    public async Task<ChatUserDto> HandleAsync(GetChatUserById query)
    {
        return await _chatUsers
            .Where(chatUser => chatUser.Id == query.Id)
            .Include(cu => cu.ChatsAsAdvertiser)
            .Include(cu => cu.ChatsAsBuyer)
            .Select(chatUser => chatUser.AsDto())
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }
}