using Mysennger.Domain.Posts.ValueObjects;

namespace Mysennger.Domain.Posts;

public interface IPostsRepository : IRepository<Post, PostId>
{
    
}