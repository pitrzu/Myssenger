using Myssenger.Shared;

namespace Mysennger.Domain.Users.ValueObjects;

public class UserId : ValueObject
{
    public Guid Value { get; }

    private UserId(Guid value)
    {
        Value = value;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public static UserId Create(Guid value)
        => new(value);

    public static UserId CreateUnique()
        => new(Guid.NewGuid());
}