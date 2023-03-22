using Mysennger.Domain.Chats.exceptions;
using Myssenger.Shared;
using OneOf.Monads;

namespace Mysennger.Domain.Chats.vo;

public sealed class Title : ValueObject
{
    public const int MaxLength = 32;
    
    private Title(string value)
    {
        Value = value;
    }
    public string Value { get; }
    
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public static Result<ArgumentException, Title> TryCreate(string content)
    {
        if (string.IsNullOrWhiteSpace(content)) 
            return new TitleLengthException();
        if (content.Length > MaxLength) 
            return new TitleLengthException(content.Length);

        return new Title(content);
    }
}