using AlledrogO.Post.Application.Contracts;

namespace AlledrogO.Post.Infrastructure.EF.Repositories;

public class PostgresPostRepository : IPostRepository
{
    
    public Task<Domain.Entities.Post> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Domain.Entities.Post post)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Domain.Entities.Post post)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Domain.Entities.Post post)
    {
        throw new NotImplementedException();
    }
}