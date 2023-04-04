using Myssenger.Shared;

namespace Mysennger.Domain.Users.ValueObjects;

public sealed class Password : ValueObject
{
    private Password(string hash)
    {
        Hash = hash;
    }
    
    public string Hash { get; }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Hash;
    }

    public static Password Create(string hash)
    {
        return new Password(hash);
    }
} 