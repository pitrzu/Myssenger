using System.Text.RegularExpressions;
using Myssenger.Shared;
using OneOf.Monads;

namespace Mysennger.Domain.Users.ValueObjects;

public sealed partial class Login : ValueObject
{
    private Login(string value)
    {
        Value = value;
    }
    
    public string Value { get; }
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public static Result<Exception, Login> TryCreate(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return new ArgumentNullException(nameof(name), $"Login can not be null or whitespace only!");
        var shouldBeEmpty = LoginRegex().Replace(name, " ");
        if (!string.IsNullOrWhiteSpace(shouldBeEmpty))
            return new ArgumentException($"Login should be lowercase letters and numbers only! Is {name}");

        return new Login(name);
    }
    
    public static Login CreateOrThrow(string name)
    {
        
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name), $"Login can not be null or whitespace only!");
        var shouldBeEmpty = LoginRegex().Replace(name, " ");
        if (!string.IsNullOrWhiteSpace(shouldBeEmpty))
            throw new ArgumentException($"Login should be lowercase letters and numbers only! Is {name}");

        return new Login(name);
    }

    [GeneratedRegex("[0-Z]*")]
    private static partial Regex LoginRegex();
}