using Mysennger.Domain.Posts.ValueObjects;
using Mysennger.Domain.Subriddots;
using Mysennger.Domain.Users.ValueObjects;
using OneOf.Monads;

namespace Mysennger.Domain.Posts.Services;

public sealed class PostsService : IPostsService
{
    public Result<Exception, Post> CreatePost(
        UserId creator, 
        Subriddot subriddot,
        PostTitle title,
        PostContent content)
    {
        var post = Post.Create(
            creator: creator,
            subriddot: subriddot.Id,
            title: title,
            content: content);
        
        subriddot._posts.Add(post.Id);

        return post;
    }
}