using Myssenger.Shared;
using OneOf.Monads;

namespace Mysennger.Domain.Subriddots.ValueObjects;

public class SubriddotDescription : ValueObject
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

    /// <summary>
    /// Static method for creating new SubriddotDescription instances
    /// throws exceptions when constrains are broken
    /// </summary>
    /// <exception cref="ArgumentNullException">When value argument is null or whitespace only</exception>
    /// <exception cref="ArgumentException">When argument is longer than max characters</exception>
    /// <param name="value">Content of the description</param>
    /// <returns>SubriddotDescription</returns>
    public static SubriddotDescription CreateOrThrow(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(nameof(value));
        if (value.Length > Constants.Subriddot.DescriptionMaxLength)
            throw new ArgumentException(
                $"Description can not be longer than {Constants.Subriddot.DescriptionMaxLength}! Is: {value.Length}");

        return new SubriddotDescription(value);
    }
    
    
    /// <summary>
    /// Static method for creating new SubriddotDescription instances
    /// returns exceptions when constraints are broken
    /// </summary>
    /// <param name="value">Content of the description</param>
    /// <returns>Discriminated union of SubriddotDescription or an Exception</returns>
    public static Result<Exception, SubriddotDescription> TryCreate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return new ArgumentNullException(nameof(value));
        if (value.Length > Constants.Subriddot.DescriptionMaxLength)
            return new ArgumentException(
                $"Description can not be longer than {Constants.Subriddot.DescriptionMaxLength}! Is: {value.Length}");

        return new SubriddotDescription(value);
    }
}