using System.Security.Cryptography;
using System.Text;

namespace Mysennger.Domain.Users.Services.Implementations;

public sealed class Sha512HashingService : IHashingService
{
    public string HashString(string password)
    {
        var bytes = Encoding.UTF8.GetBytes(password);

        var hash = SHA512.HashData(bytes);

        return Encoding.UTF8.GetString(hash);
    }
}