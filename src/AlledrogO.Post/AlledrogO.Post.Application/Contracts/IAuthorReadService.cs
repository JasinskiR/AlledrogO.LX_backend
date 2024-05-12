using AlledrogO.Post.Domain.Entities;

namespace AlledrogO.Post.Application.Contracts;

public interface IAuthorReadService
{
    Task<Author> GetAsync(Guid id);
}