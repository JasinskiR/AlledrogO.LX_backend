namespace AlledrogO.Post.Domain.Repositories;

public interface IPostRepository
{
    Task<Entities.Post> GetAsync(Guid id);
    Task AddAsync(Entities.Post post);
    Task UpdateAsync(Entities.Post post);
    Task DeleteAsync(Entities.Post post);
}