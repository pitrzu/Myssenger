using Myssenger.Shared;

namespace Mysennger.Domain.Subriddots.ValueObjects;

public sealed class SubriddotDescription : ValueObject
{
    private SubriddotDescription(string value)
    {
        Value = value;
    }
    
    public string Value { get; }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public static SubriddotDescription CreateOrThrow(string value)
    {
        return new SubriddotDescription(value);
    }
}