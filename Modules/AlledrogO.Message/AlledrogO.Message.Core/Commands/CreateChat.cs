using AlledrogO.Shared.Commands;

namespace AlledrogO.Message.Core.Commands;

public record CreateChat(Guid AdvertiserId, Guid BuyerId) : ICommand<Guid>;