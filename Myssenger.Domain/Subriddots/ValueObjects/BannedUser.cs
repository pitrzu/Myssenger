using Mysennger.Domain.Users.ValueObjects;
using Myssenger.Shared;

namespace Mysennger.Domain.Subriddots.ValueObjects;

public sealed class BannedUser : ValueObject
{
    private BannedUser(UserId id)
    {
        Id = id;
    }

    public UserId Id { get; }
    public DateTimeOffset BannedSince { get; } = DateTimeOffset.Now;
    
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Id;
    }

    public static BannedUser BanUser(UserId userId)
        => new(userId);
}