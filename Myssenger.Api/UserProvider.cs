using System.Security.Claims;
using Mysennger.Domain.Users.ValueObjects;

namespace Myssenger.Api;

public sealed class UserProvider : IUserProvider
{
    private readonly IHttpContextAccessor _context;

    public UserProvider(IHttpContextAccessor? context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public UserId GetUserId()
    {
        var httpContext = _context.HttpContext ?? throw new ArgumentNullException(nameof(_context));
        var guidString = httpContext.User.Claims.First(i => i.Type == ClaimTypes.Actor).Value;

        var guid = Guid.Parse(guidString);

        return UserId.Create(guid);
    }
}