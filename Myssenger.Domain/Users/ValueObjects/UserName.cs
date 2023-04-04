using Myssenger.Shared;
using OneOf.Monads;

namespace Mysennger.Domain.Users.ValueObjects;

public sealed class UserName : ValueObject
{
    private UserName(string value)
    {
        Value = value;
    }
    
    public string Value { get; }
    
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public static Result<Exception, UserName> TryCreate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return new ArgumentNullException(nameof(value), $"User name can not be null or whitespace only!");
        if (value.Length > Constants.User.NameMaxLength)
            return new ArgumentException(
                $"User name can not be longer than {Constants.User.NameMaxLength}! Is {value.Length}");

        return new UserName(value);
    }

    public static UserName CreateOrThrow(string value)
    {
        
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(nameof(value), $"User name can not be null or whitespace only!");
        if (value.Length > Constants.User.NameMaxLength)
            throw new ArgumentException(
                $"User name can not be longer than {Constants.User.NameMaxLength}! Is {value.Length}");

        return new UserName(value);
    }
}