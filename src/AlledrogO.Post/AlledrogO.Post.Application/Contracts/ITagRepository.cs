using AlledrogO.Post.Domain.Entities;
using AlledrogO.Post.Domain.ValueObjects;

namespace AlledrogO.Post.Application.Contracts;

public interface ITagRepository
{
    Task<Tag> GetAsync(Guid id);
    Task<Tag> GetAsync(TagName name);
    Task<IEnumerable<Tag>> GetAllAsync();
    Task AddAsync(Tag tag);
    Task UpdateAsync(Tag tag);
    Task DeleteAsync(Tag tag);
    
}