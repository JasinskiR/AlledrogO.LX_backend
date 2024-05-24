using AlledrogO.Message.Core.Entities;
using AlledrogO.Message.Core.Repositories;
using AlledrogO.Shared.MassTransit;
using AlledrogO.Shared.MassTransit.Events;
using MassTransit;

namespace AlledrogO.Message.Core.EventHandlers;

public class CreateChatUserHandler : IMessageMarker, IConsumer<UserCreatedEvent>
{
    private readonly IChatUserRepository _chatUserRepository;

    public CreateChatUserHandler(IChatUserRepository chatUserRepository)
    {
        _chatUserRepository = chatUserRepository;
    }

    public async Task Consume(ConsumeContext<UserCreatedEvent> context)
    {
        var chatUser = new ChatUser()
        {
            Id = context.Message.UserId
        };
        await _chatUserRepository.CreateAsync(chatUser);
    }
}