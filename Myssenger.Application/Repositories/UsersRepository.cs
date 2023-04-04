using Microsoft.EntityFrameworkCore;
using Mysennger.Domain.Users;
using Mysennger.Domain.Users.ValueObjects;

namespace Myssenger.Application.Repositories;

public class UsersRepository : DbGenericRepository<User, UserId>, IUsersRepository
{
    public UsersRepository(DbContext context) : base(context)
    {
    }
}