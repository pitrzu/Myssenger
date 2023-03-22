using Mysennger.Domain.Chats.exceptions;
using Myssenger.Shared;
using OneOf.Monads;

namespace Mysennger.Domain.Chats.vo;

public sealed class Content : ValueObject
{
    public const int MaxLength = 128;
    
    private Content(string value)
    {
        Value = value;
    }
    public string Value { get; }
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public static Result<ContentLengthException, Content> TryCreate(string content)
    {
        if (string.IsNullOrWhiteSpace(content))
            return new ContentLengthException();
        if (content.Length > 128)
            return new ContentLengthException(content.Length);

        return new Content(content);
    }
}