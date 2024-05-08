using AlledrogO.Post.Domain.Entities;

namespace AlledrogO.Post.Domain.Repositories;

public interface IAuthorRepository
{
    Task<Author> GetAsync(Guid id);
    Task AddAsync(Author author);
    Task UpdateAsync(Author author);
    Task DeleteAsync(Author author);
}