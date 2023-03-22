using Myssenger.Shared;

namespace Mysennger.Domain.Chats.vo;

public class ChatId : ValueObject
{
    private ChatId(Guid value)
    {
        Value = value;
    }
    
    public Guid Value { get; }
    
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public static ChatId Create(Guid value)
        => new(value);

    public static ChatId CreateUnique()
        => new(Guid.NewGuid());
}