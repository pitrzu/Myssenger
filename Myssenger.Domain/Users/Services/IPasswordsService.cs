using Mysennger.Domain.Users.ValueObjects;
using OneOf.Monads;

namespace Mysennger.Domain.Users.Services;

public interface IPasswordsService
{
    public Result<Exception, Password> CreatePassword(string password);
}