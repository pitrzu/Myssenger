using Myssenger.Shared;

namespace Mysennger.Domain.Posts.ValueObjects;

public sealed class PostId : ValueObject
{
    private PostId(Guid value)
    {
        Value = value;
    }
    
    public Guid Value { get; }
    
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public static PostId Create(Guid value)
        => new(value);

    public static PostId CreateUnique()
        => new(Guid.NewGuid());
}