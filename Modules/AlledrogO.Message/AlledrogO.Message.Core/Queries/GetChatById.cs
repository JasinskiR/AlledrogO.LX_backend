using AlledrogO.Message.Core.DTOs;
using AlledrogO.Shared.Queries;

namespace AlledrogO.Message.Core.Queries;

public record GetChatById(Guid ChatId) : IQuery<ChatDetailsDto>;