using Myssenger.Shared;
using OneOf.Monads;

namespace Mysennger.Domain.Posts.ValueObjects;

public sealed class PostContent : ValueObject
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
        return new PostContent(value);
    }
}