using Mysennger.Domain.Users.ValueObjects;

namespace Myssenger.Api;

public interface IUserProvider
{
    public UserId GetUser();
}