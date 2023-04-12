using Myssenger.Shared;

namespace Mysennger.Domain.Subriddots.ValueObjects;

public sealed class SubriddotId : ValueObject
{
    private SubriddotId(Guid value)
    {
        Value = value;
    }
    
    public Guid Value { get; }
    
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public static SubriddotId Create(Guid value)
        => new(value);

    public static SubriddotId CreateUnique()
        => new(Guid.NewGuid());
}