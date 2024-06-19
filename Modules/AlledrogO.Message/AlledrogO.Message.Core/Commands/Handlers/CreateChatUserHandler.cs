using AlledrogO.Message.Core.EF;
using AlledrogO.Message.Core.Entities;
using AlledrogO.Message.Core.Repositories;
using AlledrogO.Shared.Commands;

namespace AlledrogO.Message.Core.Commands.Handlers;

public class CreateChatUserHandler : ICommandHandler<CreateChatUser, Guid>
{
    private readonly IChatUserRepository _chatUserRepository;

    public CreateChatUserHandler(IChatUserRepository chatUserRepository)
    {
        _chatUserRepository = chatUserRepository;
    }

    public Task<Guid> HandleAsync(CreateChatUser command)
    {
        var chatUser = new ChatUser()
        {
            Id = Guid.NewGuid()
        };

        _chatUserRepository.CreateAsync(chatUser);

        return Task.FromResult(chatUser.Id);
    }
}