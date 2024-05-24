using AlledrogO.Message.Core.Entities;

namespace AlledrogO.Message.Core.Repositories;

public interface IChatRepository
{
    Task<Chat> GetByIdAsync(Guid id);
    Task<Chat> GetByUsersPairAsync(Guid advertiserId, Guid buyerId);
    Task CreateAsync(Chat chat);
    Task UpdateAsync(Chat chat);
    Task DeleteAsync(Chat chat);
}