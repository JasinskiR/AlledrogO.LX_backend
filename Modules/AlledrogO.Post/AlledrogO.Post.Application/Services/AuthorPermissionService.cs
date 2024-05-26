using AlledrogO.Post.Application.Contracts;
using AlledrogO.Post.Application.Exceptions;

namespace AlledrogO.Post.Application.Services;

public class AuthorPermissionService : IAuthorPermissionService
{
    private readonly IPostRepository _postRepository;

    public AuthorPermissionService(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<bool> CanEditPostAsync(Guid userId, Guid postId)
    {
        var post = await _postRepository.GetAsync(postId);
        if (post is null)
        {
            throw new PostNotFoundException(postId);
        }
        return post.Author.Id == userId;
    }
}