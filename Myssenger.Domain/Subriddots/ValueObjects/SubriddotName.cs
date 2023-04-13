using Myssenger.Shared;

namespace Mysennger.Domain.Subriddots.ValueObjects;

public sealed class SubriddotName : ValueObject
{
    private SubriddotName(string value)
    {
        Value = value;
    }
    
    public string Value { get; }
    
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    private static SubriddotName CreateOrThrow(string value)
    {
        return new SubriddotName(value);
    }
}