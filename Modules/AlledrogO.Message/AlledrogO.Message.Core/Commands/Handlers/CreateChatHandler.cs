using AlledrogO.Message.Core.Entities;
using AlledrogO.Message.Core.Exceptions;
using AlledrogO.Message.Core.Repositories;
using AlledrogO.Shared.Commands;

namespace AlledrogO.Message.Core.Commands.Handlers;

public class CreateChatHandler : ICommandHandler<CreateChat>
{
    private readonly IChatRepository _chatRepository;

    public CreateChatHandler(IChatRepository chatRepository)
    {
        _chatRepository = chatRepository;
    }

    public async Task HandleAsync(CreateChat command)
    {
        if (command.BuyerId == command.AdvertiserId)
        {
            throw new ChatWithYourselfException();
        }
        if (await _chatRepository.GetByUsersPairAsync(command.AdvertiserId, command.BuyerId) != null)
        {
            throw new ChatAlreadyExistsException(command.BuyerId, command.AdvertiserId);
        }
        var chat = new Chat()
        {
            Id = Guid.NewGuid(),
            AdvertiserId = command.AdvertiserId,
            BuyerId = command.BuyerId,
            Messages = new LinkedList<Entities.Message>()
        };
        await _chatRepository.CreateAsync(chat);
    }
}