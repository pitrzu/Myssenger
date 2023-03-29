using System.Collections.Immutable;
using Mysennger.Domain.Posts.Entities;
using Mysennger.Domain.Posts.Exceptions;
using Mysennger.Domain.Posts.ValueObjects;
using Mysennger.Domain.Users.ValueObjects;
using Myssenger.Shared;
using OneOf.Monads;

namespace Mysennger.Domain.Posts;

public class Post : AggregateRoot<PostId>
{
    private readonly ICollection<Comment> _comments = new List<Comment>();
    private readonly ICollection<Rating> _ratings = new List<Rating>();
    
    private Post(PostId id,
        UserId creator,
        Content content,
        ICollection<Tag> tags) : base(id)
    {
        Creator = creator;
        Content = content;
        Tags = tags;
    }
    
    public UserId Creator { get; }
    public DateTime CreatedAt { get; } = DateTime.UtcNow;
    public Content Content { get; set; }
    public IReadOnlyCollection<Comment> Comments => _comments.ToImmutableList();
    public ICollection<Tag> Tags { get; set; } 

    internal static Post CreateWithId(
        PostId postId,
        UserId userId,
        Content content,
        ICollection<Tag> tags)
    {
        return new Post(postId, userId, content, tags);
    }

    internal static Post Create(UserId userId, Content content, ICollection<Tag> tags)
    {
        return CreateWithId(
            postId: PostId.CreateUnique(),
            userId: userId,
            content: content,
            tags: tags);
    }
    
    public Result<RatingRemovalException, bool> RemoveRating(UserId userId)
    {
        var toRemove = _ratings.FirstOrDefault(rating => rating.RatedBy == userId);
        if(ReferenceEquals(null, toRemove))
            return new RatingRemovalException(userId, Id);
        
        return _ratings.Remove(toRemove);
    }
    public void Upvote(UserId userId)
    {
        RemoveRating(userId);
        var upvote = new Rating(userId, Rating.RatingType.Upvote);
        _ratings.Add(upvote);
    }
    public void DownVote(UserId userId)
    {
        RemoveRating(userId);
        var downvote = new Rating(userId, Rating.RatingType.Downvote);
        _ratings.Add(downvote);
    }

    public void CommentPost(Comment comment)
    {
        _comments.Add(comment);
    }
    public void UpdateComment(CommentId commentId, CommentContent commentContent)
    {
        var comment = _comments.FirstOrDefault(comment => comment.Id == commentId);
        if(ReferenceEquals(null, comment))
            return;
        
        comment.Content = commentContent;
    }
    public void RemoveComment(CommentId commentId)
    {
        var comment = _comments.FirstOrDefault(comment => comment.Id == commentId);
        if(ReferenceEquals(null, comment))
            return;

        _comments.Remove(comment);
    }

    public void RemoveCommentRating(CommentId commentId, UserId userId)
    {
        var comment = _comments.FirstOrDefault(comment => comment.Id == commentId);
        if (ReferenceEquals(null, comment))
            return;

        comment.RemoveRating(userId);
    }
    public void UpvoteComment(CommentId commentId, UserId userId)
    {
        var comment = _comments.FirstOrDefault(comment => comment.Id == commentId);
        if(ReferenceEquals(null, comment))
            return;

        comment.RemoveRating(userId);
        comment.Upvote(userId);
    }
    public void DownvoteComment(CommentId commentId, UserId userId)
    {
        var comment = _comments.FirstOrDefault(comment => comment.Id == commentId);
        if(ReferenceEquals(null, comment))
            return;

        comment.RemoveRating(userId);
        comment.Downvote(userId);
    }
}