using System.Collections.Immutable;
using Mysennger.Domain.Posts.ValueObjects;
using Mysennger.Domain.Subriddots.ValueObjects;
using Mysennger.Domain.Users.ValueObjects;
using Myssenger.Shared;

namespace Mysennger.Domain.Subriddots;

public sealed class Subriddot : AggregateRoot<SubriddotId>
{
    internal readonly ICollection<PostId> _posts = new List<PostId>();
    private readonly ICollection<UserId> _moderators = new List<UserId>();
    private readonly ICollection<UserId> _bannedUsers = new List<UserId>();

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
    public SubriddotName Name { get; set; }
    public SubriddotDescription Description { get; set; }

    public static Subriddot Create(UserId creator,
        SubriddotName name,
        SubriddotDescription description)
    {
        return new(
            id: SubriddotId.CreateUnique(),
            creator: creator,
            name: name,
            description: description);
    }
    
    public IReadOnlyCollection<PostId> Posts => _posts.ToImmutableList();
    public IReadOnlyCollection<UserId> Moderators => _moderators.ToImmutableList();
    public IReadOnlyCollection<UserId> BannedUsers => _bannedUsers.ToImmutableList();

    public bool DetachPost(PostId post)
    {
        return _posts.Remove(post);
    }
    
    public void PromoteToModerator(UserId user)
    {
        if (_bannedUsers.Contains(user)) 
            return;
        
        _moderators.Add(user);
    }
    public void DemoteModerator(UserId moderator)
    {
        _moderators.Remove(moderator);
    }

    public void BanUser(UserId user)
    {
        if (_moderators.Contains(user))
            return;
        
        _bannedUsers.Add(user);
    }
    public void UnBanUser(UserId bannedUser)
    {
        _bannedUsers.Remove(bannedUser);
    }
}