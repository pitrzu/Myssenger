using Mysennger.Domain.Users.ValueObjects;

namespace Mysennger.Domain.Users;

public interface IUsersRepository : IGenericRepository<User, UserId>
{
    
}