using AlledrogO.Post.Domain.ValueObjects;
using AlledrogO.Shared.Domain;

namespace AlledrogO.Post.Domain.Entities;

public class Author : AggregateRoot<Guid>
{
    private LinkedList<Post> _posts = new();
    private AuthorData _authorData;
}