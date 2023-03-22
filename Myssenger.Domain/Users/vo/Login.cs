using Myssenger.Shared;
using OneOf.Monads;

namespace Mysennger.Domain.Chats.vo;

public class Login : ValueObject
{
    public const int MaxLength = 32;

    private Login(string value)
    {
        Value = value;
    }

    
    public string Value { get; }
    
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public static Result<ArgumentException, Login> TryCreate(string content)
    {
        if (string.IsNullOrWhiteSpace(content)) ;
        if (content.Length > MaxLength) ;

        return new Login(content);
    }
}