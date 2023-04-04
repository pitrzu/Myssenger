using System.Collections.Immutable;
using Mysennger.Domain.Subriddots.ValueObjects;
using Mysennger.Domain.Users.ValueObjects;
using Myssenger.Shared;

namespace Mysennger.Domain.Users;

public sealed class User : AggregateRoot<UserId>
{
    private readonly ICollection<SubriddotId> _subscribedTo = new List<SubriddotId>();
    
    private readonly ICollection<UserId> _follows = new List<UserId>();
    private readonly ICollection<UserId> _followedBy = new List<UserId>();

    private User(UserId id,
        UserName username,
        Login login,
        Password password) : base(id)
    {
        Name = username;
        Login = login;
        Password = password;
    }
    
    public UserName Name { get; set; }
    public Login Login { get; }
    public Password Password { get; set; }
    public IReadOnlyCollection<SubriddotId> SubscribedTo => _subscribedTo.ToImmutableList();
    public IReadOnlyCollection<UserId> Follows => _follows.ToImmutableList();
    public IReadOnlyCollection<UserId> FollowedBy => _followedBy.ToImmutableList();

    internal static User CreateWithId(UserId id,
        UserName userName,
        Login login,
        Password password)
    {
        return new User(id, userName, login, password);
    }

    internal static User Create(UserName userName,
        Login login,
        Password password)
    {
        return CreateWithId(
            id: UserId.CreateUnique(),
            userName,
            login,
            password);
    } 

    public void SubscribeToSubriddot(SubriddotId subriddotId)
    {
        if(_subscribedTo.Contains(subriddotId))
            return;
        _subscribedTo.Add(subriddotId);
    }
    public void UnsubscribeFrom(SubriddotId subriddotId)
    {
        _subscribedTo.Remove(subriddotId);
    }

    public void Follow(User user)
    {
        if(_follows.Contains(user.Id))
            return;
        _follows.Add(user.Id);
        user._followedBy.Add(Id);
    }
    public void UnFollow(User user)
    {
        if(!_follows.Contains(user.Id))
            return;
        _follows.Remove(user.Id);
        user._followedBy.Remove(Id);
    }
}