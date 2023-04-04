using Mysennger.Domain.Users.ValueObjects;
using OneOf.Monads;

namespace Mysennger.Domain.Users.Services;

public interface IUsersService
{
    public Result<Exception, User> CreateUser(
        UserName userName,
        Login login,
        Password password);
}