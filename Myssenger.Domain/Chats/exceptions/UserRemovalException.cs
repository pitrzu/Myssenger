using System.Data;

namespace Mysennger.Domain.Chats.exceptions;

public class UserRemovalException : ConstraintException
{
    public UserRemovalException(int currentCount) : base($"To remove a user there must be at least two users left after the operation! Current count: {currentCount}")
    {
        
    }
}