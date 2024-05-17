using AlledrogO.Post.Application.Contracts;
using AlledrogO.Post.Domain.Factories;
using AlledrogO.Post.Domain.ValueObjects;
using AlledrogO.Shared.MassTransit.Events;
using MassTransit;

namespace AlledrogO.Post.Application.EventHandlers;

public class CreateAuthorHandler : IConsumer<UserCreatedEvent>
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IAuthorFactory _authorFactory;

    public CreateAuthorHandler(IAuthorRepository authorRepository, IAuthorFactory authorFactory)
    {
        _authorRepository = authorRepository;
        _authorFactory = authorFactory;
    }

    public async Task Consume(ConsumeContext<UserCreatedEvent> context)
    {
        var authorDetails = new AuthorDetails(
            context.Message.Email,
            context.Message.PhoneNumber);
        
        var author = _authorFactory.Create(context.Message.UserId, 
            authorDetails, Enumerable.Empty<Domain.Entities.Post>());
        
        await _authorRepository.AddAsync(author);
    }
}