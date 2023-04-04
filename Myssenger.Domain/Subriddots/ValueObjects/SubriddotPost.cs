using Mysennger.Domain.Posts.ValueObjects;
using Myssenger.Shared;

namespace Mysennger.Domain.Subriddots.ValueObjects;

public sealed class SubriddotPost : ValueObject
{
    private SubriddotPost(PostId postId, bool isPinned)
    {
        PostId = postId;
        IsPinned = isPinned;
    }
    
    public PostId PostId { get; }
    public bool IsPinned { get; }
    
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return PostId;
    }

    public static SubriddotPost CreateUnPinned(PostId postId)
        => new(postId, false);
    
    public static SubriddotPost CreatePinned(PostId postId)
        => new(postId, true);
}