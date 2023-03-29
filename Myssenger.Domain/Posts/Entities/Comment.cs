using Mysennger.Domain.Posts.ValueObjects;
using Mysennger.Domain.Users.ValueObjects;
using Myssenger.Shared;

namespace Mysennger.Domain.Posts.Entities;

public class Comment : Entity<CommentId>
{
    private readonly ICollection<Rating> _ratings = new List<Rating>();

    protected Comment(CommentId id,
        UserId creator,
        CommentContent content) : base(id)
    {
        Creator = creator;
        Content = content;
    }

    public UserId Creator { get; }
    public CommentContent Content { get; set; }

    public void RemoveRating(UserId userId)
    {
        var toRemove = _ratings.FirstOrDefault(rating => rating.RatedBy == userId);
        if (ReferenceEquals(null, toRemove))
            return;

        _ratings.Remove(toRemove);
    }
    
    public void Upvote(UserId userId)
    {
        RemoveRating(userId);
        var upvote = new Rating(userId, Rating.RatingType.Upvote); 
        
        _ratings.Add(upvote);
    }
    
    public void Downvote(UserId userId)
    {
        RemoveRating(userId);
        var downvote = new Rating(userId, Rating.RatingType.Downvote);
        
        _ratings.Add(downvote);
    }
}