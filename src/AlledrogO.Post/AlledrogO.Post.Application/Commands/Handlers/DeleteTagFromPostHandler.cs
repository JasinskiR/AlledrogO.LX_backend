using AlledrogO.Post.Application.Contracts;
using AlledrogO.Post.Application.Exceptions;
using AlledrogO.Post.Domain.ValueObjects;
using AlledrogO.Shared.Commands;

namespace AlledrogO.Post.Application.Commands.Handlers;

public class DeleteTagFromPostHandler : ICommandHandler<DeleteTagFromPost>
{
    private readonly IPostRepository _postRepository;
    private readonly ITagRepository _tagRepository;

    public DeleteTagFromPostHandler(IPostRepository postRepository, ITagRepository tagRepository)
    {
        _postRepository = postRepository;
        _tagRepository = tagRepository;
    }

    public async Task HandleAsync(DeleteTagFromPost command)
    {
        (var postId, TagName tagName) = command;
        
        var post = await _postRepository.GetAsync(postId);
        if (post is null)
        {
            throw new PostNotFoundException(postId);
        }
        
        var tag = await _tagRepository.GetAsync(tagName);
        if (tag is null)
        {
            throw new TagNotFoundException(tagName);
        }
        
        post.RemoveTag(tag);
        tag.RemovePost(post);
        if (tag.PostCount == 0)
        {
            await _tagRepository.DeleteAsync(tag);
        }
        else
        {
            await _tagRepository.UpdateAsync(tag);
        }
        await _postRepository.UpdateAsync(post);
    }
}