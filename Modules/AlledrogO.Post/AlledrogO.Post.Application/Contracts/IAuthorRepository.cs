using AlledrogO.Post.Domain.Entities;

namespace AlledrogO.Post.Application.Contracts;

public interface IAuthorRepository
{
    Task<Author> GetAsync(Guid id);
    Task AddAsync(Author author);
    Task UpdateAsync(Author author);
    Task DeleteAsync(Author author);
}