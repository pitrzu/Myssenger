using System.Collections.Immutable;
using Mysennger.Domain.Posts.ValueObjects;
using Mysennger.Domain.Subriddots.ValueObjects;
using Mysennger.Domain.Users.ValueObjects;
using Myssenger.Shared;
using OneOf.Monads;

namespace Mysennger.Domain.Subriddots;

public sealed class Subriddot : AggregateRoot<SubriddotId>
{
    private ICollection<PostId> _posts = new List<PostId>();
    private readonly ISet<Moderator> _moderators = new HashSet<Moderator>();
    private readonly ISet<UserId> _bannedUsers = new HashSet<UserId>();

    private Subriddot(
        SubriddotId id,
        Name name,
        Description description,
        UserId creator) : base(id) 
    {
        Name = name;
        Description = description;
        Creator = creator;
    }

    public UserId Creator { get; }
    public Name Name { get; set; }
    public Description Description { get; set; }
    public IReadOnlyCollection<PostId> Posts => _posts.ToImmutableList();
    public IReadOnlySet<Moderator> Moderators => _moderators.ToImmutableHashSet();
    public IReadOnlySet<UserId> BannedUsers => _bannedUsers.ToImmutableHashSet();

    public Result<Exception, PostId> AttachPost(PostId postId, UserId postCreator)
    {
        if(_bannedUsers.Contains(postCreator))
            return new Exception();

        _posts.Add(postId);
        return postId;
    }
    public bool DetachPost(PostId postId)
    {
        return _posts.Remove(postId);
    }

    public void PromoteUserToModerator(UserId userId)
    {
        if(_bannedUsers.Contains(userId))
            return;
        
        var newModerator = Moderator.Create(userId);
        _moderators.Add(newModerator);
    }
    public void DemoteModerator(UserId userId)
    {
        var moderator = _moderators.FirstOrDefault(moderator => moderator.UserId == userId);
        if(ReferenceEquals(null, moderator))
            return;

        _moderators.Remove(moderator);
    }

    public void BanUser(UserId userId)
    {
        _bannedUsers.Add(userId);
    }
    public void UnbanUser(UserId userId)
    {
        _bannedUsers.Remove(userId);
    }
}