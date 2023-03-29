using Mysennger.Domain.Posts.ValueObjects;
using Mysennger.Domain.Subriddots;
using Mysennger.Domain.Subriddots.ValueObjects;
using Mysennger.Domain.Users.ValueObjects;
using OneOf.Monads;

namespace Mysennger.Domain.Posts;

public class PostsService : IDomainService 
{
    private readonly ISubriddotsRepository _subriddotsRepository;

    public PostsService(ISubriddotsRepository subriddotsRepository)
    {
        _subriddotsRepository = subriddotsRepository;
    }

    public Result<Exception, Post> CreatePost(
        SubriddotId subriddotId,
        UserId creator,
        Content content,
        ICollection<Tag> tags)
    {
        var subriddot = _subriddotsRepository.One(subriddotId);
        if (ReferenceEquals(null, subriddot))
            return new Exception();
        
        var post = Post.Create(creator, content, tags);
        var attachmentResult = subriddot.AttachPost(post.Id, creator);
        
        if (attachmentResult.IsError())
            return attachmentResult.ErrorValue();

        return post;
    }
}