using AlledrogO.Post.Application.Contracts;
using AlledrogO.Post.Application.Exceptions;
using AlledrogO.Shared.MassTransit;
using AlledrogO.Shared.MassTransit.Events;
using MassTransit;

namespace AlledrogO.Post.Application.EventHandlers;

public class DeleteAuthorHandler : IMessageMarker, IConsumer<UserDeletedEvent>
{
    private readonly IAuthorRepository _authorRepository;

    public DeleteAuthorHandler(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task Consume(ConsumeContext<UserDeletedEvent> context)
    {
        var id = context.Message.UserId;
        var author = await _authorRepository.GetAsync(id);
        if (author is null)
        {
            throw new AuthorNotFoundException(id);
        }
        // author.DeleteAllPosts();
        await _authorRepository.DeleteAsync(author);
    }
}