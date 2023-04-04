using Microsoft.EntityFrameworkCore;
using Mysennger.Domain.Posts;
using Mysennger.Domain.Posts.ValueObjects;
using Mysennger.Domain.Users.ValueObjects;

namespace Myssenger.Application.Repositories;

public class PostsRepository : DbGenericRepository<Post, PostId>, IPostsRepository
{
    public PostsRepository(DbContext context) : base(context)
    {
    }

    public async Task<ICollection<Post>> GetAllByUserId(UserId userId)
    {
        var results = DbSet.Where(post => post.Creator == userId);
        return await results.ToListAsync();
    }
}