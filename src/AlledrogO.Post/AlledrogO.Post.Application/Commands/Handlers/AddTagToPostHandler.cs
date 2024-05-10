using AlledrogO.Post.Application.Contracts;
using AlledrogO.Post.Application.Exceptions;
using AlledrogO.Post.Domain.Factories;
using AlledrogO.Post.Domain.ValueObjects;
using AlledrogO.Shared.Commands;

namespace AlledrogO.Post.Application.Commands.Handlers;

public class AddTagToPostHandler : ICommandHandler<AddTagToPost>
{
    private readonly IPostRepository _postRepository;
    private readonly ITagRepository _tagRepository;
    private readonly ITagFactory _tagFactory;
    public AddTagToPostHandler(IPostRepository postRepository, ITagRepository tagRepository, ITagFactory tagFactory)
    {
        _postRepository = postRepository;
        _tagRepository = tagRepository;
        _tagFactory = tagFactory;
    }

    public async Task HandleAsync(AddTagToPost command)
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
            tag = _tagFactory.CreateNew(tagName, post);
            await _tagRepository.AddAsync(tag);
        }
        else
        {
            tag.AddPost(post);
            await _tagRepository.UpdateAsync(tag);
        }
        post.AddTag(tag);
        await _postRepository.UpdateAsync(post);
    }
}