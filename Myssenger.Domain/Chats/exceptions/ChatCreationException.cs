namespace Mysennger.Domain.Chats.exceptions;

public class ChatCreationException : ArgumentException
{
    public ChatCreationException(int numberOfParticipants)
        : base($"To create a chat there must be at least two participants! Is: {numberOfParticipants}")
    {
        
    }
}