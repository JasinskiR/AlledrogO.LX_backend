namespace AlledrogO.Post.Application.Contracts;

public interface IPostRepository
{
    Task<Domain.Entities.Post> GetAsync(Guid id);
    Task AddAsync(Domain.Entities.Post post);
    Task UpdateAsync(Domain.Entities.Post post);
    Task DeleteAsync(Domain.Entities.Post post);
}