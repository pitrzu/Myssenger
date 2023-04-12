using Mysennger.Domain.Posts.Entities;
using Mysennger.Domain.Posts.ValueObjects;
using Mysennger.Domain.Subriddots.ValueObjects;
using Mysennger.Domain.Users.ValueObjects;
using Myssenger.Shared;

namespace Mysennger.Domain.Posts;

public sealed class Post : AggregateRoot<PostId>
{
    private readonly ICollection<Rating> _ratings = new List<Rating>();
    private readonly ICollection<Comment> _comments = new List<Comment>();

    internal Post(
        PostId id,
        UserId creator,
        SubriddotId subriddot,
        PostTitle title,
        PostContent content) : base(id)
    {
        Creator = creator;
        Subriddot = subriddot;
        Title = title;
        Content = content;
    }
    
    public UserId Creator { get; }
    public DateTimeOffset CreatedAt { get; } = DateTimeOffset.Now;
    
    public SubriddotId Subriddot { get; }
    public PostTitle Title { get; set; }
    public PostContent Content { get; set; }

    internal void Rate(UserId userId, Rating.Type type)
    {
        RemoveRating(userId);
        
        var rating = Rating.Create(userId, type);
        _ratings.Add(rating);
    }
    internal void RemoveRating(UserId userId)
    {
        var rating = _ratings.FirstOrDefault(rating => rating.Creator == userId);
        if (rating is null)
            return;

        _ratings.Remove(rating);
    }

    //Comments
    internal void CreateComment(UserId commentCreator, CommentContent content, CommentId? parent = null)
    {
        var comment = Comment.Create(commentCreator, content, parent);
        _comments.Add(comment);
    }
    internal void UpdateComment(CommentId commentId, CommentContent content)
    {
        var commentToUpdate = _comments.FirstOrDefault(comment => comment.Id == commentId);
        if (commentToUpdate is null)
            return;

        commentToUpdate.Content = content;
    }
    internal void RemoveComment(CommentId commentId)
    {
        var commentToRemove = _comments.FirstOrDefault(comment => comment.Id == commentId);
        if (commentToRemove is null)
            return;

        _comments.Remove(commentToRemove);
    }

    internal void RateComment(CommentId commentId, UserId userId, Rating.Type type)
    {
        var commentToUpvote = _comments.FirstOrDefault(comment => comment.Id == commentId);
        if(commentToUpvote is null)
            return;
        
        RemoveCommentRating(commentId, userId);

        var rating = Rating.Create(userId, type);
        commentToUpvote._ratings.Add(rating);
    }
    internal void RemoveCommentRating(CommentId commentId, UserId userId)
    {
        var commentToRemoveRating = _comments.FirstOrDefault(comment => comment.Id == commentId);
        if (commentToRemoveRating is null)
            return;

        var rating = commentToRemoveRating._ratings.FirstOrDefault(rating => rating.Creator == userId);
        if (rating is null)
            return;

        commentToRemoveRating._ratings.Remove(rating);
    }
}