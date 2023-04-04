using Myssenger.Shared;
using OneOf.Monads;

namespace Mysennger.Domain.Posts.ValueObjects;

public class CommentContent : ValueObject
{
    private CommentContent(string value)
    {
        Value = value;
    }
    
    public string Value { get; }
    
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
    
    /// <summary>
    /// Static method for creating new CommentContent instances
    /// throws exceptions when constrains are broken
    /// </summary>
    /// <exception cref="ArgumentNullException">When value argument is null or whitespace only</exception>
    /// <exception cref="ArgumentException">When argument is longer than max characters</exception>
    /// <param name="value">Content of the description</param>
    /// <returns>SubriddotDescription</returns>
    public static CommentContent CreateOrThrow(string value)
    {
        
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(nameof(value));
        if (value.Length > Constants.Comment.ContentMaxLength)
            throw new ArgumentException(
                $"Comment content can not be longer than: {Constants.Comment.ContentMaxLength}! Is: {Constants.Comment.ContentMaxLength}");

        return new CommentContent(value);
    }
    
    /// <summary>
    /// Static method for creating new CommentContent instances
    /// returns exceptions when constrains are broken
    /// </summary>
    /// <exception cref="ArgumentNullException">When value argument is null or whitespace only</exception>
    /// <exception cref="ArgumentException">When argument is longer than max characters</exception>
    /// <param name="value">Content of the description</param>
    /// <returns>Discriminated union of CommentContent or an Exception</returns>
    public static Result<Exception, CommentContent> TryCreate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return new ArgumentNullException(nameof(value));
        if (value.Length > Constants.Comment.ContentMaxLength)
            return new ArgumentException(
                $"Comment content can not be longer than: {Constants.Comment.ContentMaxLength}! Is: {Constants.Comment.ContentMaxLength}");

        return new CommentContent(value);
    }
}