using Mysennger.Domain.Posts.ValueObjects;
using Mysennger.Domain.Users;
using Mysennger.Domain.Users.ValueObjects;
using Myssenger.Shared;

namespace Mysennger.Domain.Posts.Entities;

public sealed class Comment : Entity<CommentId>
{
    private readonly ICollection<Rating> _ratings = new List<Rating>();
    
    private Comment(CommentId id,
        UserId creator,
        CommentContent content) : base(id)
    {
        Creator = creator;
        Content = content;
    }

    public UserId Creator { get; }
    public CommentContent Content { get; set; }

    public static Comment CreateWithId(
        CommentId id,
        UserId creator,
        CommentContent content)
    {
        return new Comment(id, creator, content);
    }

    public static Comment Create(UserId creator, CommentContent content)
    {
        return CreateWithId(
            id: CommentId.CreateUnique(),
            creator,
            content);
    }
}