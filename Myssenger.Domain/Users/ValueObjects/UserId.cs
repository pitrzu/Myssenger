using Myssenger.Shared;

namespace Mysennger.Domain.Users.ValueObjects;

public sealed class UserId : ValueObject
{
    private UserId(Guid value)
    {
        Value = value;
    }
    
    public Guid Value { get; }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public static UserId Create(Guid value)
        => new(value);

    public static UserId CreateUnique(Guid value)
        => new(Guid.NewGuid());
}