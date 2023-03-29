using Mysennger.Domain.Posts.ValueObjects;
using Mysennger.Domain.Users.ValueObjects;

namespace Mysennger.Domain.Posts.Exceptions;

public class RatingRemovalException : RatingException 
{
    public RatingRemovalException(UserId userId, PostId postId)
        : base($"There are no ratings from user: {userId} in post: {postId}") { }
    
    public RatingRemovalException(UserId userId, CommentId commentId)
        : base($"There are no ratings from user: {userId} in comment: {commentId}") { }
}