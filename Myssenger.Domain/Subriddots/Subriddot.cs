using System.Collections.Immutable;
using System.Data;
using Mysennger.Domain.Posts.ValueObjects;
using Mysennger.Domain.Subriddots.ValueObjects;
using Mysennger.Domain.Users.ValueObjects;
using Myssenger.Shared;
using OneOf.Monads;

namespace Mysennger.Domain.Subriddots;

public sealed class Subriddot : AggregateRoot<SubriddotId>
{
    private readonly ICollection<SubriddotPost> _posts = new HashSet<SubriddotPost>();
    private readonly ICollection<SubriddotModerator> _moderators = new HashSet<SubriddotModerator>();
    private readonly ICollection<BannedUser> _bannedUsers = new HashSet<BannedUser>();

    private Subriddot(
        SubriddotId id,
        SubriddotName subriddotName,
        SubriddotDescription subriddotDescription,
        UserId creator) : base(id) 
    {
        SubriddotName = subriddotName;
        SubriddotDescription = subriddotDescription;
        Creator = creator;
    }

    public UserId Creator { get; }
    public SubriddotName SubriddotName { get; set; }
    public SubriddotDescription SubriddotDescription { get; set; }
    public DateTimeOffset CreatedAt { get; } = DateTimeOffset.Now;
    public IReadOnlyCollection<SubriddotPost> Posts => _posts.ToImmutableList();
    public IReadOnlySet<SubriddotModerator> Moderators => _moderators.ToImmutableHashSet();
    public IReadOnlySet<BannedUser> BannedUsers => _bannedUsers.ToImmutableHashSet();

    public static Subriddot CreateWithId(
        SubriddotId id,
        UserId creator,
        SubriddotName subriddotName,
        SubriddotDescription subriddotDescription)
    {
        return new(
            id: id,
            subriddotName: subriddotName,
            subriddotDescription: subriddotDescription,
            creator: creator
        );
    }
    public static Subriddot Create(
        UserId creator,
        SubriddotName subriddotName,
        SubriddotDescription subriddotDescription)
    {
        return CreateWithId(
            id: SubriddotId.CreateUnique(),
            creator: creator,
            subriddotName: subriddotName,
            subriddotDescription: subriddotDescription);
    }

    public Result<Exception, PostId> AttachPost(PostId postId, UserId creator)
    {
        if (_bannedUsers.Any(user => user.Id == Creator))
            return new ConstraintException(
                $"User {creator} is banned from subriddot {Id}, and can not post there!");
        if (_posts.Any(subriddotPost => subriddotPost.PostId == postId))
            return new ArgumentException($"The post {postId} is already present in subriddot {Id}");
        
        var postToAttach = SubriddotPost.CreateUnPinned(postId);
        _posts.Add(postToAttach);
        
        return postId;
    }
    public Result<Exception, PostId> DetachPost(PostId postId)
    {
        var postToRemove = _posts.FirstOrDefault(post => post.PostId == postId);
        if (ReferenceEquals(null, postToRemove))
            return new ArgumentException($"There is no post {postId} in subriddot {Id} so it can not be detached!", nameof(postId));

        _posts.Remove(postToRemove);
        return postId;
    }
    
    public Result<Exception, PostId> PinPost(PostId postId)
    {
        var postToPin = _posts.FirstOrDefault(post => post.PostId == postId);
        if(ReferenceEquals(null, postToPin))
            return new ArgumentException($"There is no post {postId} in subriddot {Id} so it can not be pinned!", nameof(postId));

        _posts.Remove(postToPin);
        var pinnedPost = SubriddotPost.CreatePinned(postToPin.PostId);
        _posts.Add(pinnedPost);

        return postId;
    }
    public Result<Exception, PostId> UnPinPost(PostId postId)
    {
        var postToUnPin = _posts.FirstOrDefault(post => post.PostId == postId);
        if(ReferenceEquals(null, postToUnPin))
            return new Exception($"There is no post {postId} in subriddot {Id} so it can not be unpinned!");

        _posts.Remove(postToUnPin);
        var pinnedPost = SubriddotPost.CreateUnPinned(postToUnPin.PostId);
        _posts.Add(pinnedPost);

        return postId;
    }
   
    public Result<Exception, UserId> BanUser(UserId userId)
    {
        if (_moderators.Any(user => user.UserId == userId))
            return new ConstraintException($"The user {userId} is a moderator in subriddot {Id} so he can not be banned!");

        var bannedUser = BannedUser.BanUser(userId);
        _bannedUsers.Add(bannedUser);

        return userId;
    }
    public Result<Exception, UserId> UnBanUser(UserId userId)
    {
        var userToUnBan = _bannedUsers.FirstOrDefault(user => user.Id == userId);
        if (ReferenceEquals(null, userToUnBan))
            return new ArgumentException($"The user {userId} is not banned in subriddot {Id} so he can not be unbanned!", nameof(userId));

        _bannedUsers.Remove(userToUnBan);
        return userId;
    }
    
    public Result<Exception, UserId> PromoteUserToModerator(UserId userId)
    {
        if(_bannedUsers.Any(user => user.Id == userId))
            return new ConstraintException($"The user {userId} is banned in subriddot {Id} so he can not be promoted to a moderator!");

        var moderator = SubriddotModerator.Create(userId);
        _moderators.Add(moderator);

        return userId;
    }
    public Result<Exception, UserId> DemoteModerator(UserId userId)
    {
        var moderatorToDemote = _moderators.FirstOrDefault(moderator => moderator.UserId == userId);
        if(ReferenceEquals(null, moderatorToDemote))
            return new ArgumentException($"The user {userId} is not a moderator in subriddot {Id} so he can not be demoted from a moderator!", nameof(userId));

        _moderators.Remove(moderatorToDemote);
        return userId;
    }
}