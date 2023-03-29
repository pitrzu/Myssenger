using Myssenger.Shared;

namespace Mysennger.Domain.Subriddots.ValueObjects;

public class SubriddotId : ValueObject
{
    public Guid Value { get; }

    private SubriddotId(Guid value)
    {
        Value = value;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public static SubriddotId Create(Guid value)
        => new(value);

    public static SubriddotId CreateUnique()
        => new(Guid.NewGuid());
}