using System.Data;
using Mysennger.Domain.Chats.vo;

namespace Mysennger.Domain.Chats.exceptions;

public class UserBannedException : ConstraintException
{
    public UserBannedException(UserId userId, ChatId chatId)
        : base($"User {userId} is banned in chat {chatId} and can not be added as a participant!")
    {
        
    }
}