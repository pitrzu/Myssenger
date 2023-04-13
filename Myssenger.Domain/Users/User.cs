using System.Collections.Immutable;
using Mysennger.Domain.Subriddots.ValueObjects;
using Mysennger.Domain.Users.ValueObjects;
using Myssenger.Shared;

namespace Mysennger.Domain.Users;

public sealed class User : AggregateRoot<UserId>
{
    private readonly ICollection<SubriddotId> _subscribedTo = new List<SubriddotId>();

    private User(
        UserId id,
        Login login,
        EMail eMail,
        Password password) : base(id)
    {
        Login = login;
        EMail = eMail;
        Password = password;
    }
    
    public Login Login { get; }
    public EMail EMail { get; set; }
    public Password Password { get; set; }
    public IReadOnlyCollection<SubriddotId> SubscribedTo => _subscribedTo.ToImmutableList();

    public static User Create(Login login, EMail eMail, Password password)
    {
        return new(
            id: UserId.CreateUnique(),
            login: login,
            eMail: eMail,
            password: password);
    }
    
    public void Subscribe(SubriddotId subriddot)
    {
        _subscribedTo.Add(subriddot);
    }
    public void UnSubscribe(SubriddotId subriddot)
    {
        _subscribedTo.Remove(subriddot);
    }
}