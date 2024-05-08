using AlledrogO.Post.Domain.Factories;
using AlledrogO.Post.Domain.Repositories;
using AlledrogO.Shared.Commands;

namespace AlledrogO.Post.Application.Commands.Handlers;

public class CreatePostHandler : ICommandHandler<CreatePost>
{
    private readonly IPostRepository _postRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly IPostFactory _postFactory;

    public CreatePostHandler(IPostRepository postRepository, IAuthorRepository authorRepository, IPostFactory postFactory)
    {
        _postRepository = postRepository;
        _authorRepository = authorRepository;
        _postFactory = postFactory;
    }

    public async Task HandleAsync(CreatePost command)
    {
        var (id, title, description, authorId) = command;
        var author = await _authorRepository.GetAsync(authorId);
        if (author is null)
        {
            throw new AuthorNotFoundException(authorId);
        }
    }
}