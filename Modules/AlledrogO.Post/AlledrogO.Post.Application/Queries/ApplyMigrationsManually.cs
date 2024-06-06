using System.Windows.Input;
using AlledrogO.Shared.Queries;

namespace AlledrogO.Post.Application.Queries;

public record ApplyMigrationsManually() : IQuery<bool>;