using AlledrogO.Post.Application.Contracts;
using AlledrogO.Post.Application.Exceptions;
using AlledrogO.Shared.Commands;

namespace AlledrogO.Post.Application.Commands.Handlers;

public class DeleteAuthorHandler : ICommandHandler<DeleteAuthor>
{
    private readonly IAuthorRepository _authorRepository;

    public DeleteAuthorHandler(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task HandleAsync(DeleteAuthor command)
    {
        var id = command.Id;
        var author = await _authorRepository.GetAsync(id);
        if (author is null)
        {
            throw new AuthorNotFoundException(id);
        }
        // author.DeleteAllPosts();
        await _authorRepository.DeleteAsync(author);
    }
}