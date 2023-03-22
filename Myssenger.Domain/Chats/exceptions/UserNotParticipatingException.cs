using System.Data;
using Mysennger.Domain.Chats.vo;

namespace Mysennger.Domain.Chats.exceptions;

public class UserNotParticipatingException : ConstraintException 
{
    public UserNotParticipatingException(UserId userId, ChatId chatId) 
        : base($"User: {userId} can not send a message in chat {chatId} because he does not participate in it!")
    {
        
    }
}