namespace Mysennger.Domain.Users.Services;

public interface IHashingService
{
    public string HashString(string password);
}