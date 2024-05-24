using AlledrogO.Message.Core.Entities;

namespace AlledrogO.Message.Core.Repositories;

public interface IChatUserRepository
{
    Task<ChatUser> GetByIdAsync(Guid id);
    Task CreateAsync(ChatUser chatUser);
    Task UpdateAsync(ChatUser chatUser);
    Task DeleteAsync(ChatUser chatUser);
}