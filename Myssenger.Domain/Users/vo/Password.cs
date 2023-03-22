using Myssenger.Shared;

namespace Mysennger.Domain.Chats.vo;

public class Password : ValueObject
{
    private Password(string hash, string salt)
    {
        Hash = hash;
        Salt = salt;
    }
    
    public string Hash { get; }
    public string Salt { get; }
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Hash;
        yield return Salt;
    }

    public static Password Create(string hash, string salt)
    {
        return new Password(hash, salt);
    }
}