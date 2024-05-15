using System.Windows.Input;
using AlledrogO.Post.Application.Contracts;
using AlledrogO.Post.Domain.Factories;
using AlledrogO.Post.Domain.ValueObjects;
using AlledrogO.Shared.Commands;

namespace AlledrogO.Post.Application.Commands.Handlers;

public class CreateAuthorHandler : ICommandHandler<CreateAuthor, Guid>
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IAuthorFactory _authorFactory;

    public CreateAuthorHandler(IAuthorRepository authorRepository, IAuthorFactory authorFactory)
    {
        _authorRepository = authorRepository;
        _authorFactory = authorFactory;
    }


    public async Task<Guid> HandleAsync(CreateAuthor command)
    {
        var id = Guid.NewGuid();
        var authorDetails = new AuthorDetails(
            command.AuthorDetails.Email,
            command.AuthorDetails.PhoneNumber);
        var author = _authorFactory.Create(id, authorDetails, Enumerable.Empty<Domain.Entities.Post>());
        await _authorRepository.AddAsync(author);
        return author.Id;
    }
}