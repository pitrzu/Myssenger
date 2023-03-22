using Mysennger.Domain.Chats.vo;

namespace Mysennger.Domain.Chats.exceptions;

public class MessageNotFoundException : ArgumentException
{
    public MessageNotFoundException(MessageId messageId, ChatId chatId)
        : base($"There is no message {messageId} in chat {chatId}")
    {
        
    }
}