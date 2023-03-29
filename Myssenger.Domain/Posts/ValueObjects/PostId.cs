using Myssenger.Shared;

namespace Mysennger.Domain.Posts.ValueObjects;

public class PostId : ValueObject
{
    public Guid Value { get; }

    private PostId(Guid value)
    {
        Value = value;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public static PostId Create(Guid value)
        => new(value);

    public static PostId CreateUnique()
        => new(Guid.NewGuid());
}