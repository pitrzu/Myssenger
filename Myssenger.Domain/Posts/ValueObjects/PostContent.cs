using Myssenger.Shared;
using OneOf.Monads;

namespace Mysennger.Domain.Posts.ValueObjects;

public class PostContent : ValueObject
{
    private PostContent(string value)
    {
        Value = value;
    }
    
    public string Value { get; }
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public static Result<Exception, PostContent> TryCreate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return new Exception();

        return new PostContent(value);
    }
}
