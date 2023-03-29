using Mysennger.Domain.Subriddots.ValueObjects;
using Mysennger.Domain.Users.ValueObjects;
using Myssenger.Shared;

namespace Mysennger.Domain.Users;

public class User : AggregateRoot<UserId>
{
    private readonly ICollection<SubriddotId> _subscribedTo = new List<SubriddotId>();

    protected User(UserId id) : base(id)
    {
    }
}