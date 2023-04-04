using System.Text.RegularExpressions;
using Mysennger.Domain.Users.ValueObjects;
using OneOf.Monads;

namespace Mysennger.Domain.Users.Services.Implementations;

public sealed partial class PasswordsService : IPasswordsService
{
    private readonly IHashingService _hashingService;

    public PasswordsService(IHashingService hashingService)
    {
        _hashingService = hashingService;
    }

    public Result<Exception, Password> CreatePassword(string password)
    {
        if (password.Length < Constants.User.PasswordMinLength)
            return new ArgumentException(
                $"Password must be at least {Constants.User.PasswordMinLength}! Is {password.Length}");
        if (!PasswordRegex().IsMatch(password))
            return new ArgumentException($"Password must contain one uppercase, one lowercase letter, one digit and one special character! Is {password}");
        
        var hash = _hashingService.HashString(password);

        return Password.Create(password);
    }

    [GeneratedRegex("^(?=.{8,})(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=.]).*$")]
    private static partial Regex PasswordRegex();
}