using Myssenger.Shared;
using OneOf.Monads;

namespace Mysennger.Domain.Subriddots.ValueObjects;

public class SubriddotName : ValueObject
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

    public static SubriddotName CreateOrThrow(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name));
        if (name.Length > Constants.Subriddot.NameMaxLength)
            throw new ArgumentException($"Name can not be longer than {Constants.Subriddot.NameMaxLength} characters! Is: {name.Length}", nameof(name));

        return new SubriddotName(name);
    }

    public static Result<Exception, SubriddotName> TryCreate(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return new ArgumentNullException(nameof(name));
        if (name.Length > Constants.Subriddot.NameMaxLength)
            return new ArgumentException($"Name can not be longer than {Constants.Subriddot.NameMaxLength} characters! Is: {name.Length}", nameof(name));

        return new SubriddotName(name);
    }
}