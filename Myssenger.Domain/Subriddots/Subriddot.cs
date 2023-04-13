using System.Collections.Immutable;
using Mysennger.Domain.Posts.ValueObjects;
using Mysennger.Domain.Subriddots.ValueObjects;
using Mysennger.Domain.Users.ValueObjects;
using Myssenger.Shared;

namespace Mysennger.Domain.Subriddots;

public sealed class Subriddot : AggregateRoot<SubriddotId>
{
    internal readonly ICollection<PostId> _posts = new List<PostId>();
    
    private Subriddot(
        SubriddotId id,
        UserId creator,
        SubriddotName name,
        SubriddotDescription description) : base(id)
    {
        Creator = creator;
        Name = name;
        Description = description;
    }
    
    public UserId Creator { get; }
    public DateTimeOffset CreatedAt { get; } = DateTimeOffset.Now;
    public SubriddotName Name { get; set; }
    public SubriddotDescription Description { get; set; }
    public IReadOnlyCollection<PostId> Posts => _posts.ToImmutableList();

    public static Subriddot Create(
        UserId creator,
        SubriddotName name,
        SubriddotDescription description)
    {
        return new(
            id: SubriddotId.CreateUnique(),
            creator: creator,
            name: name,
            description: description);
    }
    
    public bool DetachPost(PostId post)
    {
        return _posts.Remove(post);
    }
}