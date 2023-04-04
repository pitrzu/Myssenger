using Mysennger.Domain.Users.Services;
using Mysennger.Domain.Users.Services.Implementations;

namespace Myssenger.Tests.Domain.Users;

public sealed class PasswordTests
{
    private IPasswordsService _passwordsService = null!;

    [SetUp]
    public void SetUp()
    {
        _passwordsService = new PasswordsService(new Sha512HashingService());
    }

    [Test]
    public void LessThan8CharsReturnsException()
    {
        var result = _passwordsService.CreatePassword("1234Aa.");
        Assert.That(result.IsError(), Is.True);
    }

    [Test]
    public void RegexMismatchReturnsException()
    {
        var result = _passwordsService.CreatePassword("12345678");
        Assert.That(result.IsError(), Is.True);
    }

    [Test]
    public void GoodPasswordShouldReturnHash()
    {
        var result = _passwordsService.CreatePassword("12345Aa.");
        Assert.That(result.IsSuccess(), Is.True);
    }
}