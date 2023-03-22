using Myssenger.Shared;

namespace Mysennger.Domain.Chats.vo;

public sealed class MessageId : ValueObject
{
    private MessageId(Guid value)
    {
        Value = value;
    }
    
    public Guid Value { get; }
    
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public static MessageId Create(Guid value)
        => new(value);

    public static MessageId CreateUnique()
        => new(Guid.NewGuid());
}