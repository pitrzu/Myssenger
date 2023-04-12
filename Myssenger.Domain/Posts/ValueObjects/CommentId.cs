using Myssenger.Shared;

namespace Mysennger.Domain.Posts.ValueObjects;
public sealed class CommentId : ValueObject
{
    private CommentId(Guid value)
    {
        Value = value;
    }
    
    public Guid Value { get; }
    
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public static CommentId Create(Guid value)
        => new(value);

    public static CommentId CreateUnique()
        => new(Guid.NewGuid());
}
