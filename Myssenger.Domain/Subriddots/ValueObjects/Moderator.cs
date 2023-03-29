using Mysennger.Domain.Users.ValueObjects;
using Myssenger.Shared;

namespace Mysennger.Domain.Subriddots.ValueObjects;

public class Moderator : ValueObject
{
    private Moderator(UserId userId)
    {
        UserId = userId;
    }

    
    public UserId UserId { get; }
    public DateTime PromotedAt { get; } = DateTime.UtcNow;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return UserId;
    }

    public static Moderator Create(UserId userId)
        => new(userId);
}