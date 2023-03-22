using Myssenger.Shared;

namespace Mysennger.Domain.Chats.vo;

public class UserId : ValueObject
{
    private UserId(Guid value)
    {
        Value = value;
    }
    
    public Guid Value { get; }
    
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public static UserId Create(Guid value)
        => new(value);

    public static UserId CreateUnique()
        => new(Guid.NewGuid());
}