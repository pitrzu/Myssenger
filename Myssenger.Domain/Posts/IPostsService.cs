using Mysennger.Domain.Posts.Exceptions;
using OneOf.Monads;

namespace Mysennger.Domain.Posts;

public interface IPostsService : IDomainService
{
    public Result<PostCreationException, Post> CreatePost();
}