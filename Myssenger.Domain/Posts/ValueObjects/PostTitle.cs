using Myssenger.Shared;
using OneOf.Monads;

namespace Mysennger.Domain.Posts.ValueObjects;

public sealed class PostTitle : ValueObject
{
    private PostTitle(string value)
    {
        Value = value;
    }
    
    public string Value { get; }
    
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public static Result<Exception, PostTitle> CreateOrThrow(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return new Exception();
        if (value.Length > Constants.Comment.ContentMaxLength)
            return new Exception();

        return new PostTitle(value);
    }
}