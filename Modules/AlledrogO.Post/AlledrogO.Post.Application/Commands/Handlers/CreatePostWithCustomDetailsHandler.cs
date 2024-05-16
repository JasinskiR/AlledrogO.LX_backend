using AlledrogO.Post.Application.Contracts;
using AlledrogO.Post.Application.Exceptions;
using AlledrogO.Post.Domain.Factories;
using AlledrogO.Post.Domain.ValueObjects;
using AlledrogO.Shared.Commands;

namespace AlledrogO.Post.Application.Commands.Handlers;

public class CreatePostWithCustomDetailsHandler : ICommandHandler<CreatePostWithCustomDetails>
{
    private readonly IPostRepository _postRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly IPostFactory _postFactory;


    public CreatePostWithCustomDetailsHandler(IPostRepository postRepository, IAuthorRepository authorRepository, IPostFactory postFactory)
    {
        _postRepository = postRepository;
        _authorRepository = authorRepository;
        _postFactory = postFactory;
    }

    public async Task HandleAsync(CreatePostWithCustomDetails command)
    {
        var (title, description, authorId, authotDetails) = command;
        var author = await _authorRepository.GetAsync(authorId);
        if (author is null)
        {
            throw new AuthorNotFoundException(authorId);
        }
        var details = new AuthorDetails(authotDetails.Email, authotDetails.PhoneNumber);
        var id = Guid.NewGuid();
        var post = _postFactory.CreateWithCustomDetails(id, title, description, author, details);
        author.AddPost(post);
        var t1 = _postRepository.AddAsync(post);
        var t2 = _authorRepository.UpdateAsync(author);
        await Task.WhenAll(t1, t2);
    }

}