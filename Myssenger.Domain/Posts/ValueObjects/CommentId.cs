using Myssenger.Shared;

namespace Mysennger.Domain.Posts.ValueObjects;

public class CommentId : ValueObject
{
    public Guid Value { get; }

    private CommentId(Guid value)
    {
        Value = value;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public static CommentId Create(Guid value)
        => new(value);

    public static CommentId CreateUnique()
        => new(Guid.NewGuid());
}
