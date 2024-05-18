namespace AlledrogO.Post.Application.Services;

public interface IAuthorPermissionService
{
    Task<bool> CanEditPostAsync(Guid userId, Guid postId);
}