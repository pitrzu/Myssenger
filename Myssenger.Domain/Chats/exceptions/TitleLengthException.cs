using Mysennger.Domain.Chats.vo;

namespace Mysennger.Domain.Chats.exceptions;

public class TitleLengthException : ArgumentException
{
    public TitleLengthException()
        : base($"Title must not be empty or whitespace only!")
    {
    }

    public TitleLengthException(int length)
        : base($"Title's length must be {Title.MaxLength} long! Is {length}")
    {
        
    }
}