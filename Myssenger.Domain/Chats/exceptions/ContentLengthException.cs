using Mysennger.Domain.Chats.vo;

namespace Mysennger.Domain.Chats.exceptions;

public class ContentLengthException : ArgumentException
{
    public ContentLengthException()
        : base($"Content must not be empty or whitespace only!")
    {
    }

    public ContentLengthException(int length)
        : base($"Content's length must be {Content.MaxLength} long! Is {length}")
    {
        
    }
}