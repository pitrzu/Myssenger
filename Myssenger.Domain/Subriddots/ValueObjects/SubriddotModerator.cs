using Mysennger.Domain.Users.ValueObjects;
using Myssenger.Shared;

namespace Mysennger.Domain.Subriddots.ValueObjects;

public class SubriddotModerator : ValueObject
{
    private SubriddotModerator(UserId userId)
    {
        UserId = userId;
    }

    public UserId UserId { get; }
    public DateTimeOffset PromotedAt { get; } = DateTime.Now;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return UserId;
    }

    public static SubriddotModerator Create(UserId userId)
        => new(userId);
}