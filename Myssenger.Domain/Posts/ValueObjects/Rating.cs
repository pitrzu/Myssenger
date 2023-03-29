using Mysennger.Domain.Users.ValueObjects;
using Myssenger.Shared;

namespace Mysennger.Domain.Posts.ValueObjects;

public sealed class Rating : ValueObject
{
    public Rating(UserId ratedBy, RatingType type)
    {
        RatedBy = ratedBy;
        Type = type;
    }
    
    public enum RatingType
    {
        Upvote,
        Downvote
    }
    
    public UserId RatedBy { get; }
    public RatingType Type { get; }
    
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return RatedBy;
    }
}