using Mysennger.Domain.Posts.ValueObjects;
using Mysennger.Domain.Subriddots;
using Mysennger.Domain.Users.ValueObjects;
using OneOf.Monads;

namespace Mysennger.Domain.Posts.Services;

public interface IPostsService
{
    public Result<Exception, Post> CreatePost(
        UserId creator,
        Subriddot subriddot,
        PostTitle title,
        PostContent content);
}