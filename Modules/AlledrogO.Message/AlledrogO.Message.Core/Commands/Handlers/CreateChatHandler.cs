using AlledrogO.Message.Core.Entities;
using AlledrogO.Message.Core.Exceptions;
using AlledrogO.Message.Core.Repositories;
using AlledrogO.Shared.Commands;

namespace AlledrogO.Message.Core.Commands.Handlers;

public class CreateChatHandler : ICommandHandler<CreateChat, Guid>
{
    private readonly IChatRepository _chatRepository;
    private readonly IChatUserRepository _chatUserRepository;

    public CreateChatHandler(IChatRepository chatRepository, IChatUserRepository chatUserRepository)
    {
        _chatRepository = chatRepository;
        _chatUserRepository = chatUserRepository;
    }

    public async Task<Guid> HandleAsync(CreateChat command)
    {
        if (command.BuyerId == command.AdvertiserId)
        {
            throw new ChatWithYourselfException();
        }
        if (await _chatRepository.GetByUsersPairAsync(command.AdvertiserId, command.BuyerId) != null)
        {
            throw new ChatAlreadyExistsException(command.BuyerId, command.AdvertiserId);
        }

        if (await _chatUserRepository.GetByIdAsync(command.BuyerId) == null 
            || await _chatUserRepository.GetByIdAsync(command.AdvertiserId) == null)
        {
            throw new ChatUserNotFoundException(command.BuyerId);
        }

        var chat = new Chat()
        {
            Id = Guid.NewGuid(),
            AdvertiserId = command.AdvertiserId,
            BuyerId = command.BuyerId,
            Messages = new LinkedList<Entities.Message>()
        };
        await _chatRepository.CreateAsync(chat);
        return chat.Id;
    }
}