using Mysennger.Domain.Users.ValueObjects;

namespace Mysennger.Domain.Posts.Services;

public interface IPostUserAuthService : IDomainService
{
    public bool CanModify(UserId userId);
}