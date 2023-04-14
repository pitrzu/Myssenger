using Mysennger.Domain.Posts.ValueObjects;
using Myssenger.Shared;

namespace Mysennger.Domain.Posts;

public interface IPostsRepository : IRepository<Post, PostId>
{
    public Task<PaginatedList<Post>> PopularPosts(int? page = null, int? perPage = null);
}