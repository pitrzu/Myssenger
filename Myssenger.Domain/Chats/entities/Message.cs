using Myssenger.Shared;

namespace Mysennger.Domain.Chats.vo;

public sealed class Message : Entity<MessageId>
{
    private Message(
        MessageId id,
        UserId sender,
        Content content) : base(id)
    {
        Sender = sender;
        Content = content;
    }
    
    public UserId Sender { get; }
    public Content Content { get; set; }

    public static Message Create(UserId userId, Content content)
    {
        return new Message(MessageId.CreateUnique(), userId, content);
    }
}