using AlledrogO.Shared.Commands;
using Microsoft.AspNetCore.Http;

namespace AlledrogO.Post.Application.Commands;

public record AddPostImage(Guid PostId, IFormFile Image) : ICommand;