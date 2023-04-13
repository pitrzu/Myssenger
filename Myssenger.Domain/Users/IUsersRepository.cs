using Mysennger.Domain.Users.ValueObjects;

namespace Mysennger.Domain.Users;

public interface IUsersRepository : IRepository<User, UserId>
{
    public Task<IEnumerable<User>> GetAllBySimilarName(string name);
}