using Mysennger.Domain.Users.ValueObjects;
using Myssenger.Shared;

namespace Mysennger.Domain.Posts.ValueObjects;

public class Rating : ValueObject
{
    public enum Type
    {
        Upvote,
        Downvote
    }

    private Rating(UserId creator, Type ratingType)
    {
        Creator = creator;
        RatingType = ratingType;
    }
    
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Creator;
    }

    public UserId Creator { get; }
    public Type RatingType { get; }

    public static Rating Create(UserId creator, Type type)
        => new(creator, type);
}