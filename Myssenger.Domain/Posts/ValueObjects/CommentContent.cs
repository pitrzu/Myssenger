using Myssenger.Shared;

namespace Mysennger.Domain.Posts.ValueObjects;

public sealed class CommentContent : ValueObject
{
    private CommentContent(string value)
    {
        Value = value;
    }
    
    public string Value { get; }


    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public static CommentContent Create(string content)
    {
        return new(content);
    }
}