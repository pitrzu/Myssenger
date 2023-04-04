using Mysennger.Domain.Posts.ValueObjects;
using Mysennger.Domain.Users.ValueObjects;

namespace Mysennger.Domain.Posts;

public interface IPostsRepository : IGenericRepository<Post, PostId>
{
    public Task<ICollection<Post>> GetAllByUserId(UserId userId);
}