using Myssenger.Shared;
using OneOf.Monads;

namespace Mysennger.Domain.Posts.ValueObjects;

public class Content : ValueObject
{
    private Content(string value)
    {
        Value = value;
    }
    
    public string Value { get; }
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public static Result<Exception, Content> TryCreate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return new Exception();

        return new Content(value);
    }
}
