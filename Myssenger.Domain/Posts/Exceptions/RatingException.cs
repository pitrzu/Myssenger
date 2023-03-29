namespace Mysennger.Domain.Posts.Exceptions;

public class RatingException : ArgumentException
{
    public RatingException() {}
    public RatingException(string message) : base(message) { }
}