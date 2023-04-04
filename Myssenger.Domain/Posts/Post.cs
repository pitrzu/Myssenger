using System.Collections.Immutable;
using Mysennger.Domain.Posts.Entities;
using Mysennger.Domain.Posts.ValueObjects;
using Mysennger.Domain.Users.ValueObjects;
using Myssenger.Shared;

namespace Mysennger.Domain.Posts;

public class Post : AggregateRoot<PostId>
{
    private readonly ICollection<Comment> _comments = new List<Comment>();
    private readonly ICollection<Rating> _ratings = new List<Rating>();
    
    private Post(PostId id,
        UserId creator,
        PostContent postContent,
        ICollection<Tag> tags) : base(id)
    {
        Creator = creator;
        PostContent = postContent;
        Tags = tags;
    }
    
    public UserId Creator { get; }
    public DateTime CreatedAt { get; } = DateTime.UtcNow;
    public PostContent PostContent { get; set; }
    public IReadOnlyCollection<Comment> Comments => _comments.ToImmutableList();
    public ICollection<Tag> Tags { get; set; } 

    internal static Post CreateWithId(
        PostId postId,
        UserId userId,
        PostContent postContent,
        ICollection<Tag> tags)
    {
        return new Post(postId, userId, postContent, tags);
    }

    internal static Post Create(UserId userId, PostContent postContent, ICollection<Tag> tags)
    {
        return CreateWithId(
            postId: PostId.CreateUnique(),
            userId: userId,
            postContent: postContent,
            tags: tags);
    }
}