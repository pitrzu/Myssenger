using System.Collections.Immutable;
using Mysennger.Domain.Posts.ValueObjects;
using Mysennger.Domain.Users.ValueObjects;
using Myssenger.Shared;

namespace Mysennger.Domain.Posts.Entities;

public sealed class Comment : Entity<CommentId>
{
    internal readonly ICollection<Rating> _ratings = new List<Rating>();

    private Comment(
        CommentId id, 
        CommentId? parent,
        UserId creator,
        CommentContent content) : base(id)
    {
        Parent = parent;
        Creator = creator;
        Content = content;
    }
    
    public CommentId? Parent { get; }
    public UserId Creator { get; }
    public DateTimeOffset CreatedAt { get; } = DateTimeOffset.Now;
    public CommentContent Content { get; internal set; }
    public IReadOnlyCollection<Rating> Ratings => _ratings.ToImmutableList();

    public static Comment Create(UserId creator, CommentContent content, CommentId? parent = null)
    {
        return new(
            id: CommentId.CreateUnique(),
            parent: parent,
            creator: creator,
            content: content);
    }
}