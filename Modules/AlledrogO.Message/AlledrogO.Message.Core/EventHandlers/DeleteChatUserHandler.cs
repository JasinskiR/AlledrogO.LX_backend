using AlledrogO.Message.Core.Repositories;
using AlledrogO.Shared.MassTransit;
using AlledrogO.Shared.MassTransit.Events;
using MassTransit;

namespace AlledrogO.Message.Core.EventHandlers;

public class DeleteChatUserHandler : IMessageMarker, IConsumer<UserDeletedEvent>
{
    private readonly IChatUserRepository _chatUserRepository;

    public DeleteChatUserHandler(IChatUserRepository chatUserRepository)
    {
        _chatUserRepository = chatUserRepository;
    }

    public async Task Consume(ConsumeContext<UserDeletedEvent> context)
    {
        var chatUser = await _chatUserRepository.GetByIdAsync(context.Message.UserId);
        await _chatUserRepository.DeleteAsync(chatUser);
    }
}