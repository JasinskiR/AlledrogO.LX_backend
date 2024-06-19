using System.Windows.Input;
using AlledrogO.Message.Core.DTOs;
using AlledrogO.Shared.Queries;

namespace AlledrogO.Message.Core.Queries;

public record GetChatUserById(Guid Id) : IQuery<ChatUserDto>;
