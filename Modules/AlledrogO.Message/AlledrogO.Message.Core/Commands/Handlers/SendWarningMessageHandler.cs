using AlledrogO.Message.Core.Entities;
using AlledrogO.Message.Core.Exceptions;
using AlledrogO.Message.Core.Repositories;
using AlledrogO.Shared.Commands;
using Microsoft.Extensions.Configuration;

namespace AlledrogO.Message.Core.Commands.Handlers;

public class SendWarningMessageHandler : ICommandHandler<SendWarningMessage>
{
    private readonly IChatUserRepository _chatUserRepository;
    private readonly IChatRepository _chatRepository;
    private readonly IConfiguration _configuration;

    public SendWarningMessageHandler(IChatUserRepository chatUserRepository, 
        IChatRepository chatRepository, IConfiguration configuration)
    {
        _chatUserRepository = chatUserRepository;
        _chatRepository = chatRepository;
        _configuration = configuration;
    }

    public async Task HandleAsync(SendWarningMessage command)
    {
        var platformEmail = _configuration.GetSection("PlatformEmail").Value
            ?? throw new ArgumentNullException("PlatformEmail is not set in appsettings.json");
        var platformUser = await _chatUserRepository.GetByEmailAsync(platformEmail);
        if (platformUser == null)
        {
            throw new ChatUserNotFoundException(platformEmail);
        }
        var reciever = await _chatUserRepository.GetByEmailAsync(command.email);
        if (reciever == null)
        {
            throw new ChatUserNotFoundException(command.email);
        }
        var message = new Entities.Message()
        {
            CreatedAt = DateTime.Now,
            Content = command.message,
            SentByBuyer = false
        };
        
        var platformChat = await _chatRepository.GetByUsersPairAsync(advertiserId: platformUser.Id,
            buyerId: reciever.Id);
        if (platformChat is null)
        {
            var chat = new Chat()
            {
                Id = Guid.NewGuid(),
                Advertiser = platformUser,
                Buyer = reciever,
                Messages = new LinkedList<Entities.Message>(new[] { message })
            };
            await _chatRepository.CreateAsync(chat);
        }
        else
        {
            platformChat.Messages.AddLast(message);
            await _chatRepository.UpdateAsync(platformChat);
        }
    }
}