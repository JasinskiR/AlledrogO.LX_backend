using AlledrogO.Post.Application.Contracts;
using AlledrogO.Post.Application.Exceptions;
using AlledrogO.Post.Domain.Factories;
using AlledrogO.Shared.Commands;

namespace AlledrogO.Post.Application.Commands.Handlers;

public class CreatePostHandler : ICommandHandler<CreatePost, Guid>
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

    public async Task<Guid> HandleAsync(CreatePost command)
    {
        var ( title, description, authorId) = command;
        var author = await _authorRepository.GetAsync(authorId);
        if (author is null)
        {
            throw new AuthorNotFoundException(authorId);
        }
        var id = Guid.NewGuid();
        var post = _postFactory.Create(id, title, description, author);
        author.AddPost(post);
        var t1 = _postRepository.AddAsync(post);
        var t2 = _authorRepository.UpdateAsync(author);
        await Task.WhenAll(t1, t2);
        return id;
    }
}